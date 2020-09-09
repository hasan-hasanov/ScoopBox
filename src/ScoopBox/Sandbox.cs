using ScoopBox.CommandTranslators;
using ScoopBox.CommandTranslators.Powershell;
using ScoopBox.SandboxConfigurations;
using ScoopBox.SandboxProcesses;
using ScoopBox.SandboxProcesses.Cmd;
using ScoopBox.ScriptBuilders.Powershell;
using ScoopBox.ScriptGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly Dictionary<Type, Func<FileSystemInfo, string, string>> _translatorsFactory;
        private readonly IOptions _options;
        private readonly ISandboxProcess _sandboxProcess;
        private readonly IScriptGenerator _scriptGenerator;
        private readonly ISandboxConfigurationBuilder _sandboxConfigurationBuilder;
        private readonly IFileSystem _fileSystem;

        public Sandbox()
            : this(
                  new Options(),
                  new SandboxCmdProcess(),
                  new SandboxConfigurationBuilder(new Options()))
        {
        }

        public Sandbox(IOptions options)
            : this(
                  options,
                  new SandboxCmdProcess(options.RootFilesDirectoryLocation, options.SandboxConfigurationFileName),
                  new SandboxConfigurationBuilder(options))
        {
        }

        public Sandbox(ISandboxProcess scoopBoxProcess)
            : this(
                  new Options(),
                  new SandboxCmdProcess(),
                  new SandboxConfigurationBuilder(new Options()))
        {
        }

        public Sandbox(ISandboxConfigurationBuilder sandboxConfigurationBuilder)
            : this(
                  new Options(),
                  new SandboxCmdProcess(),
                  sandboxConfigurationBuilder)
        {
        }

        public Sandbox(
            IOptions options,
            ISandboxProcess sandboxProcess,
            IScriptGenerator scriptGenerator)
            : this(options, sandboxProcess, scriptGenerator, new SandboxConfigurationBuilder(options), new FileSystem())
        {
        }

        public Sandbox(
            IOptions options,
            ISandboxProcess sandboxProcess,
            ISandboxConfigurationBuilder sandboxConfigurationBuilder)
            : this(options, sandboxProcess, new PowershellScriptGenerator(), sandboxConfigurationBuilder, new FileSystem())
        {
        }

        public Sandbox(
            IOptions options,
            ISandboxProcess sandboxProcess,
            IScriptGenerator scriptGenerator,
            ISandboxConfigurationBuilder sandboxConfigurationBuilder,
            IFileSystem fileSystem)
        {
            _options = options;
            _sandboxProcess = sandboxProcess ?? throw new ArgumentNullException(nameof(sandboxProcess));
            _scriptGenerator = scriptGenerator ?? throw new ArgumentNullException(nameof(scriptGenerator));
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder ?? throw new ArgumentNullException(nameof(sandboxConfigurationBuilder));
            _fileSystem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));

            _translatorsFactory = new Dictionary<Type, Func<FileSystemInfo, string, string>>()
            {
                { typeof(PowershellScriptGenerator), (file, path) => new PowershellTranslator().Translate(file, path) }
            };

            InitializeDirectoryStructure();
        }

        public Task Run(string literalScript)
        {
            return Run(new List<string>() { literalScript });
        }

        public Task Run(FileSystemInfo script, ICommandTranslator commandTranslator)
        {
            return Run(new List<Tuple<FileSystemInfo, ICommandTranslator>>() { Tuple.Create(script, commandTranslator) });
        }

        public async Task Run(List<string> literalScripts)
        {
            string content = string.Join(Environment.NewLine, literalScripts);
            FileSystemInfo file = await _scriptGenerator.Generate(_options.RootFilesDirectoryLocation, content);

            var command = _translatorsFactory[_scriptGenerator.GetType()](file, _options.RootSandboxFilesDirectoryLocation);
            _sandboxConfigurationBuilder.AddCommand(command);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(List<Tuple<FileSystemInfo, ICommandTranslator>> scripts)
        {
            List<string> commands = new List<string>();
            foreach ((FileSystemInfo file, ICommandTranslator commandTranslator) in scripts)
            {
                string sandboxScriptPath = Path.Combine(_options.RootFilesDirectoryLocation, file.Name);

                _fileSystem.File.Copy(file.FullName, sandboxScriptPath, true);
                var sandboxFile = new FileInfo(sandboxScriptPath);

                commands.Add(commandTranslator.Translate(sandboxFile, _options.RootSandboxFilesDirectoryLocation));
            }

            string content = string.Join(Environment.NewLine, commands);
            FileSystemInfo baseScriptFile = await _scriptGenerator.Generate(_options.RootFilesDirectoryLocation, content);

            var baseCommands = _translatorsFactory[_scriptGenerator.GetType()](baseScriptFile, _options.RootSandboxFilesDirectoryLocation);

            _sandboxConfigurationBuilder.AddCommand(baseCommands);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        private void InitializeDirectoryStructure()
        {
            // TODO: Think if this really should stay that way!!!
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
