using ScoopBox.PackageManager;
using ScoopBox.SandboxConfigurations;
using ScoopBox.SandboxProcesses;
using ScoopBox.Scripts;
using ScoopBox.Scripts.Powershell;
using ScoopBox.Translators;
using ScoopBox.Translators.Powershell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly IOptions _options;
        private readonly ISandboxProcess _sandboxProcess;
        private readonly ISandboxConfigurationBuilder _sandboxConfigurationBuilder;

        private readonly Action<string> _createDirectory;
        private readonly Func<string, IEnumerable<FileInfo>> _enumerateFiles;
        private readonly Func<string, IEnumerable<DirectoryInfo>> _enumerateDirectories;

        public Sandbox()
            : this(
                  new Options(),
                  new SandboxCmdProcess(),
                  new SandboxConfigurationBuilder(new Options()))
        {
        }

        private Sandbox(
            IOptions options,
            ISandboxProcess sandboxProcess,
            ISandboxConfigurationBuilder sandboxConfigurationBuilder)
        {
            _options = options;
            _sandboxProcess = sandboxProcess;
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder;

            _createDirectory = rootFilesDirectoryLocation =>
            {
                Directory.CreateDirectory(_options.RootFilesDirectoryLocation);
            };

            _enumerateFiles = rootFilesDirectoryLocation =>
            {
                return new DirectoryInfo(rootFilesDirectoryLocation).EnumerateFiles();
            };

            _enumerateDirectories = rootFilesDirectoryLocation =>
            {
                return new DirectoryInfo(rootFilesDirectoryLocation).EnumerateDirectories();
            };

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

            await _sandboxProcess.StartAsync();
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

            await _sandboxProcess.StartAsync();
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
