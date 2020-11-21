using ScoopBox.Abstractions;
using ScoopBox.SandboxConfigurations;
using ScoopBox.Scripts;
using ScoopBox.Scripts.UnMaterialized;
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
        private readonly ISandboxConfigurationBuilder _sandboxConfigurationBuilder;

        private readonly Action<string> _createDirectory;
        private readonly Action<string> _deleteFiles;
        private readonly Action<string> _deleteDirectories;
        private readonly Func<string, Task> _startProcess;
        private readonly Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> _literalScriptFactory;

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
            ISandboxConfigurationBuilder sandboxConfigurationBuilder)
            : this(
                  options,
                  sandboxConfigurationBuilder,
                  FileSystemAbstractions.CreateDirectory,
                  FileSystemAbstractions.DeleteFilesInDirectory,
                  FileSystemAbstractions.DeleteDirectoriesInDirectory,
                  ProcessAbstractions.StartCmdWithInput,
                  ScriptFactories.ProcessLiteralScriptFactory)
        {
        }

        internal Sandbox(
            IOptions options,
            ISandboxConfigurationBuilder sandboxConfigurationBuilder,
            Action<string> createDirectory,
            Action<string> deleteFiles,
            Action<string> deleteDirectories,
            Func<string, Task> startProcess,
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory)
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

            if (deleteFiles == null)
            {
                throw new ArgumentNullException(nameof(deleteFiles));
            }

            if (deleteDirectories == null)
            {
                throw new ArgumentNullException(nameof(deleteDirectories));
            }

            if (startProcess == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (literalScriptFactory == null)
            {
                throw new ArgumentNullException(nameof(literalScriptFactory));
            }

            _options = options;
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder;

            _createDirectory = createDirectory;
            _deleteFiles = deleteFiles;
            _deleteDirectories = deleteDirectories;
            _startProcess = startProcess;
            _literalScriptFactory = literalScriptFactory;

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

            LiteralScript baseScript = await _literalScriptFactory(translatedScripts, new PowershellTranslator(), "MainScript.ps1", _options);
            string baseScriptTranslator = baseScript.Translator.Translate(baseScript.ScriptFile, _options);

            await _sandboxConfigurationBuilder.Build(baseScriptTranslator);
            await _startProcess(Path.Combine(_options.RootFilesDirectoryLocation, _options.SandboxConfigurationFileName));
        }

        private void InitializeDirectoryStructure()
        {
            _createDirectory(_options.RootFilesDirectoryLocation);
            _deleteFiles(_options.RootFilesDirectoryLocation);
            _deleteDirectories(_options.RootFilesDirectoryLocation);
        }
    }
}
