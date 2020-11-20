using ScoopBox.SandboxConfigurations;
using ScoopBox.Scripts;
using ScoopBox.Scripts.UnMaterialized;
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
            : this(new Options())
        {
        }

        public Sandbox(IOptions options)
            : this(
                  options,
                  new SandboxConfigurationBuilder(new Options()))
        {
        }

        public Sandbox(
            IOptions options,
            SandboxConfigurationBuilder sandboxConfigurationBuilder)
            : this(
                  options,
                  sandboxConfigurationBuilder,
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

        public Task Run(IScript script, CancellationToken cancellationToken = default)
        {
            return Run(new List<IScript>() { script }, cancellationToken);
        }

        public async Task Run(List<IScript> scripts, CancellationToken cancellationToken = default)
        {
            List<string> translatedScripts = new List<string>();
            foreach (IScript script in scripts)
            {
                await script.Process(_options);
                translatedScripts.Add(script.Translator.Translate(script.ScriptFile, _options));
            }

            LiteralScript baseScript = new LiteralScript(translatedScripts, new PowershellTranslator(), "MainScript.ps1");
            await baseScript.Process(_options);
            string baseScriptTranslator = baseScript.Translator.Translate(baseScript.ScriptFile, _options);

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
