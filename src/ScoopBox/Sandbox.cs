using ScoopBox.CommandBuilders;
using ScoopBox.PackageManager;
using ScoopBox.SandboxConfigurations;
using ScoopBox.SandboxProcesses;
using ScoopBox.SandboxProcesses.Cmd;
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
            ISandboxConfigurationBuilder sandboxConfigurationBuilder)
            : this(options, sandboxProcess, sandboxConfigurationBuilder, new FileSystem())
        {
        }

        public Sandbox(
            IOptions options,
            ISandboxProcess sandboxProcess,
            ISandboxConfigurationBuilder sandboxConfigurationBuilder,
            IFileSystem fileSystem)
        {
            _options = options;
            _sandboxProcess = sandboxProcess ?? throw new ArgumentNullException(nameof(sandboxProcess));
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder ?? throw new ArgumentNullException(nameof(sandboxConfigurationBuilder));
            _fileSystem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));

            InitializeDirectoryStructure();
        }

        public Task Run(string literalScript, ICommandBuilder commandBuilder)
        {
            return Run(new List<Tuple<string, ICommandBuilder>>() { Tuple.Create(literalScript, commandBuilder) });
        }

        public Task Run(FileStream script, ICommandBuilder commandBuilder)
        {
            return Run(new List<Tuple<FileStream, ICommandBuilder>>() { Tuple.Create(script, commandBuilder) });
        }

        public async Task Run(List<Tuple<string, ICommandBuilder>> literalScripts)
        {
            await LiteralScriptsGeneration(literalScripts);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(List<Tuple<FileStream, ICommandBuilder>> scripts)
        {
            await BeforeScriptsGeneration(scripts);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            await PackageManagerScriptsGeneration(packageManagers);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public Task Run(string literalScriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            return Run(new List<Tuple<string, ICommandBuilder>>() { Tuple.Create(literalScriptBefore, commandBuilderBefore) }, packageManagers);
        }

        public Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            return Run(new List<Tuple<FileStream, ICommandBuilder>>() { Tuple.Create(scriptBefore, commandBuilderBefore) }, packageManagers);
        }

        public Task Run(List<Tuple<string, ICommandBuilder>> literalScriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            throw new NotImplementedException();
        }

        public async Task Run(List<Tuple<FileStream, ICommandBuilder>> scriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            await BeforeScriptsGeneration(scriptsBefore);
            await PackageManagerScriptsGeneration(packageManagers);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers, string literalScriptAfter, ICommandBuilder commandBuilderAfter)
        {
            return Run(packageManagers, new List<Tuple<string, ICommandBuilder>>() { Tuple.Create(literalScriptAfter, commandBuilderAfter) });
        }

        public Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers, FileStream scriptAfter, ICommandBuilder commandBuilderAfter)
        {
            return Run(packageManagers, new List<Tuple<FileStream, ICommandBuilder>>() { Tuple.Create(scriptAfter, commandBuilderAfter) });
        }

        public async Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers, List<Tuple<string, ICommandBuilder>> literalScriptsAfter)
        {
            await PackageManagerScriptsGeneration(packageManagers);
            await LiteralScriptsGeneration(literalScriptsAfter);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers, List<Tuple<FileStream, ICommandBuilder>> scriptsAfter)
        {
            await PackageManagerScriptsGeneration(packageManagers);
            await AfterScriptsGeneration(scriptsAfter);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public Task Run(string literalScriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, string literalScriptAfter, ICommandBuilder commandBuilderAfter)
        {
            return Run(
                new List<Tuple<string, ICommandBuilder>>() { Tuple.Create(literalScriptBefore, commandBuilderBefore) },
                packageManagers,
                new List<Tuple<string, ICommandBuilder>>() { Tuple.Create(literalScriptAfter, commandBuilderAfter) });
        }

        public Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, FileStream scriptAfter, ICommandBuilder commandBuilderAfter)
        {
            return Run(
                new List<Tuple<FileStream, ICommandBuilder>>() { Tuple.Create(scriptBefore, commandBuilderBefore) },
                packageManagers,
                new List<Tuple<FileStream, ICommandBuilder>>() { Tuple.Create(scriptAfter, commandBuilderAfter) });
        }

        public async Task Run(List<Tuple<string, ICommandBuilder>> literalScriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, List<Tuple<string, ICommandBuilder>> literalScriptsAfter)
        {
            await LiteralScriptsGeneration(literalScriptsBefore);
            await PackageManagerScriptsGeneration(packageManagers);
            await LiteralScriptsGeneration(literalScriptsAfter);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(List<Tuple<FileStream, ICommandBuilder>> scriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, List<Tuple<FileStream, ICommandBuilder>> scriptsAfter)
        {
            await BeforeScriptsGeneration(scriptsBefore);
            await PackageManagerScriptsGeneration(packageManagers);
            await AfterScriptsGeneration(scriptsAfter);

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

            _fileSystem.Directory.CreateDirectory($"{_fileSystem.Path.Combine(_options.RootFilesDirectoryLocation, "BeforeScripts")}");
            _fileSystem.Directory.CreateDirectory($"{_fileSystem.Path.Combine(_options.RootFilesDirectoryLocation, "AfterScripts")}");
            _fileSystem.Directory.CreateDirectory($"{_fileSystem.Path.Combine(_options.RootFilesDirectoryLocation, "PackageManagerScripts")}");
        }

        private async Task LiteralScriptsGeneration(List<Tuple<string, ICommandBuilder>> literalScripts)
        {
            foreach ((string script, ICommandBuilder commandBuilder) in literalScripts)
            {
                IEnumerable<string> commands = await commandBuilder.Build(script);
                _sandboxConfigurationBuilder.AddCommands(commands);
            }
        }

        private async Task BeforeScriptsGeneration(List<Tuple<FileStream, ICommandBuilder>> scripts)
        {
            foreach ((FileStream file, ICommandBuilder commandBuilder) in scripts)
            {
                IEnumerable<string> commands = await commandBuilder.Build(
                    file,
                    PathResolvers.GetBeforeScriptsPath(_options.RootFilesDirectoryLocation),
                    PathResolvers.GetBeforeScriptsPath(_options.RootSandboxFilesDirectoryLocation));
                _sandboxConfigurationBuilder.AddCommands(commands);
            }
        }

        private async Task AfterScriptsGeneration(List<Tuple<FileStream, ICommandBuilder>> scripts)
        {
            foreach ((FileStream file, ICommandBuilder commandBuilder) in scripts)
            {
                IEnumerable<string> commands = await commandBuilder.Build(
                    file,
                    PathResolvers.GetAfterScriptsPath(_options.RootFilesDirectoryLocation),
                    PathResolvers.GetAfterScriptsPath(_options.RootSandboxFilesDirectoryLocation));
                _sandboxConfigurationBuilder.AddCommands(commands);
            }
        }

        private async Task PackageManagerScriptsGeneration(IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            foreach (var packageManager in packageManagers)
            {
                IEnumerable<string> commands = await packageManager.Value.Build(
                    packageManager.Key,
                    PathResolvers.GetPackageManagerScriptsPath(_options.RootFilesDirectoryLocation),
                    PathResolvers.GetPackageManagerScriptsPath(_options.RootSandboxFilesDirectoryLocation));
                _sandboxConfigurationBuilder.AddCommands(commands);
            }
        }
    }
}
