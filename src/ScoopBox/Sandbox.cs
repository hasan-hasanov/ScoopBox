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
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly IOptions _options;
        private readonly ISandboxProcess _sandboxProcess;
        private readonly ISandboxConfigurationBuilder _sandboxConfigurationBuilder;
        private readonly IFileSystem _fileSystem;

        public Sandbox()
            : this(
                  new Options(),
                  new SandboxCmdProcess(),
                  new SandboxConfigurationBuilder(new Options()))
        {
        }

        private Sandbox(
            IOptions options,
            ISandboxProcess process,
            ISandboxConfigurationBuilder sandboxConfigurationBuilder)
            : this(
                  options,
                  process,
                  sandboxConfigurationBuilder,
                  new FileSystem())
        {
        }

        private Sandbox(
            IOptions options,
            ISandboxProcess sandboxProcess,
            ISandboxConfigurationBuilder sandboxConfigurationBuilder,
            IFileSystem fileSystem)
        {
            _options = options;
            _sandboxProcess = sandboxProcess;
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder;
            _fileSystem = fileSystem;

            InitializeDirectoryStructure();
        }

        public Task Run(string literalScript)
        {
            return Run(new List<string>() { literalScript });
        }

        public async Task Run(List<string> literalScripts)
        {
            BasePowershellScript baseScript = new BasePowershellScript(_options, literalScripts);
            await baseScript.GenerateScript();

            string baseScriptTranslator = new PowershellTranslator().Translate(baseScript.ScriptFile, _options.RootSandboxFilesDirectoryLocation);
            await _sandboxConfigurationBuilder.Build(baseScriptTranslator);

            await _sandboxProcess.StartAsync();
        }

        public Task Run(IPackageManager packageManager)
        {
            return Run(packageManager, new PowershellTranslator());
        }

        public Task Run(IScript script, IPowershellTranslator translator)
        {
            return Run(new List<Tuple<IScript, IPowershellTranslator>>()
            {
                Tuple.Create(script, translator)
            });
        }

        public async Task Run(List<Tuple<IScript, IPowershellTranslator>> scripts)
        {
            List<string> translatedScripts = new List<string>();
            foreach ((IScript script, IPowershellTranslator translator) in scripts)
            {
                if (Path.GetDirectoryName(script.ScriptFile.FullName) != _options.RootFilesDirectoryLocation)
                {
                    string sandboxScriptPath = Path.Combine(_options.RootFilesDirectoryLocation, script.ScriptFile.Name);
                    _fileSystem.File.Copy(script.ScriptFile.FullName, sandboxScriptPath, true);
                    script.ScriptFile = new FileInfo(sandboxScriptPath);
                    translatedScripts.Add(translator.Translate(script.ScriptFile, _options.RootSandboxFilesDirectoryLocation));
                }
            }

            BasePowershellScript baseScript = new BasePowershellScript(_options, translatedScripts);
            await baseScript.GenerateScript();

            string baseScriptTranslator = new PowershellTranslator().Translate(baseScript.ScriptFile, _options.RootSandboxFilesDirectoryLocation);
            await _sandboxConfigurationBuilder.Build(baseScriptTranslator);

            await _sandboxProcess.StartAsync();
        }

        private void InitializeDirectoryStructure()
        {
            _fileSystem.Directory.CreateDirectory(_options.RootFilesDirectoryLocation);

            foreach (IFileInfo file in _fileSystem.DirectoryInfo.FromDirectoryName(_options.RootFilesDirectoryLocation).EnumerateFiles())
            {
                file.Delete();
            }

            foreach (IDirectoryInfo dir in _fileSystem.DirectoryInfo.FromDirectoryName(_options.RootFilesDirectoryLocation).EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}
