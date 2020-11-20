using ScoopBox.PackageManager;
using ScoopBox.SandboxConfigurations;
using ScoopBox.Scripts;
using ScoopBox.Scripts.Powershell;
using ScoopBox.Translators;
using ScoopBox.Translators.Powershell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly IOptions _options;
        private readonly ISandboxConfigurationBuilder _sandboxConfigurationBuilder;

        private readonly Action<string> _createDirectory;
        private readonly Func<string, IEnumerable<FileInfo>> _enumerateFiles;
        private readonly Func<string, IEnumerable<DirectoryInfo>> _enumerateDirectories;
        private readonly Func<string, Task> _startProcess;

        public Sandbox()
            : this(
                  new Options(),
                  new SandboxConfigurationBuilder(new Options()),
                  path => Directory.CreateDirectory(path),
                  path => new DirectoryInfo(path).EnumerateFiles(),
                  path => new DirectoryInfo(path).EnumerateDirectories(),
                  async path =>
                  {
                      var process = new Process()
                      {
                          StartInfo = new ProcessStartInfo()
                          {
                              FileName = "cmd.exe",
                              RedirectStandardInput = true,
                              RedirectStandardOutput = true,
                              CreateNoWindow = false,
                              WindowStyle = ProcessWindowStyle.Hidden,
                              UseShellExecute = false,
                          }
                      };

                      process.Start();
                      await process.StandardInput.WriteLineAsync($"\"{path}\"");
                      await process.StandardInput.FlushAsync();
                      process.StandardInput.Close();
                      process.WaitForExit();
                  })
        {
        }

        internal Sandbox(
            IOptions options,
            ISandboxConfigurationBuilder sandboxConfigurationBuilder,
            Action<string> createDirectory,
            Func<string, IEnumerable<FileInfo>> enumerateFiles,
            Func<string, IEnumerable<DirectoryInfo>> enumerateDirectories,
            Func<string, Task> startProcess)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (sandboxConfigurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(sandboxConfigurationBuilder));
            }

            if (createDirectory == null)
            {
                throw new ArgumentNullException(nameof(createDirectory));
            }

            if (enumerateFiles == null)
            {
                throw new ArgumentNullException(nameof(enumerateFiles));
            }

            if (enumerateDirectories == null)
            {
                throw new ArgumentNullException(nameof(enumerateDirectories));
            }

            if (startProcess == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options;
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder;

            _createDirectory = createDirectory;
            _enumerateFiles = enumerateFiles;
            _enumerateDirectories = enumerateDirectories;
            _startProcess = startProcess;

            InitializeDirectoryStructure();
        }

        public Task Run(string literalScript, CancellationToken cancellationToken = default)
        {
            return Run(new List<string>() { literalScript }, cancellationToken);
        }

        public async Task Run(List<string> literalScripts, CancellationToken cancellationToken = default)
        {
            BasePowershellScript baseScript = new BasePowershellScript(_options, literalScripts);
            await baseScript.CopyOrMaterialize(_options, cancellationToken);

            string baseScriptTranslator = new PowershellTranslator().Translate(baseScript.ScriptFile, _options.RootSandboxFilesDirectoryLocation);
            await _sandboxConfigurationBuilder.Build(baseScriptTranslator, cancellationToken);

            await _startProcess(Path.Combine(_options.RootFilesDirectoryLocation, _options.SandboxConfigurationFileName));
        }

        public Task Run(IPackageManager packageManager, CancellationToken cancellationToken = default)
        {
            return Run(packageManager, new PowershellTranslator(), cancellationToken);
        }

        public Task Run(IScript script, IPowershellTranslator translator, CancellationToken cancellationToken = default)
        {
            return Run(new List<Tuple<IScript, IPowershellTranslator>>()
            {
                Tuple.Create(script, translator)
            }, cancellationToken);
        }

        public async Task Run(List<Tuple<IScript, IPowershellTranslator>> scripts, CancellationToken cancellationToken = default)
        {
            List<string> translatedScripts = new List<string>();
            foreach ((IScript script, IPowershellTranslator translator) in scripts)
            {
                await script.CopyOrMaterialize(_options);
                translatedScripts.Add(translator.Translate(script.ScriptFile, _options.RootSandboxFilesDirectoryLocation));
            }

            BasePowershellScript baseScript = new BasePowershellScript(_options, translatedScripts);
            await baseScript.CopyOrMaterialize(_options);

            string baseScriptTranslator = new PowershellTranslator().Translate(baseScript.ScriptFile, _options.RootSandboxFilesDirectoryLocation);
            await _sandboxConfigurationBuilder.Build(baseScriptTranslator);

            await _startProcess(Path.Combine(_options.RootFilesDirectoryLocation, _options.SandboxConfigurationFileName));
        }

        private void InitializeDirectoryStructure()
        {
            _createDirectory(_options.RootFilesDirectoryLocation);

            foreach (FileInfo file in _enumerateFiles(_options.RootFilesDirectoryLocation))
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in _enumerateDirectories(_options.RootFilesDirectoryLocation))
            {
                dir.Delete(true);
            }
        }
    }
}
