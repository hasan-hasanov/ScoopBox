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

        public async Task Run()
        {
            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(
            FileStream script,
            ICommandBuilder commandBuilder)
        {
            await Run(new Dictionary<FileStream, ICommandBuilder>() { { script, commandBuilder } });
        }

        public async Task Run(IDictionary<FileStream, ICommandBuilder> scripts)
        {
            GenerateBeforeScripts(scripts);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            await GeneratePackageManagerScripts(packageManagers);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(
            FileStream scriptBefore,
            ICommandBuilder commandBuilder,
            IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            await Run(
                new Dictionary<FileStream, ICommandBuilder>() { { scriptBefore, commandBuilder } },
                packageManagers);
        }

        public async Task Run(
            IDictionary<FileStream, ICommandBuilder> scriptsBefore,
            IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            GenerateBeforeScripts(scriptsBefore);
            await GeneratePackageManagerScripts(packageManagers);

            await _sandboxConfigurationBuilder.CreateConfigurationFile();
            await _sandboxProcess.StartAsync();
        }

        public async Task Run(
            FileStream scriptBefore,
            ICommandBuilder commandBuilderBefore,
            IDictionary<IPackageManager, ICommandBuilder> packageManagers,
            FileStream scriptAfter,
            ICommandBuilder commandBuilderAfter)
        {
            await Run(
                new Dictionary<FileStream, ICommandBuilder>() { { scriptBefore, commandBuilderBefore } },
                packageManagers,
                new Dictionary<FileStream, ICommandBuilder>() { { scriptAfter, commandBuilderAfter } });
        }

        public async Task Run(
            IDictionary<FileStream, ICommandBuilder> scriptsBefore,
            IDictionary<IPackageManager, ICommandBuilder> packageManagers,
            FileStream scriptAfter,
            ICommandBuilder commandBuilderAfter)
        {
            await Run(
                scriptsBefore,
                packageManagers,
                new Dictionary<FileStream, ICommandBuilder>() { { scriptAfter, commandBuilderAfter } });
        }

        public async Task Run(
            FileStream scriptBefore,
            ICommandBuilder commandBuilderBefore,
            IDictionary<IPackageManager, ICommandBuilder> packageManagers,
            IDictionary<FileStream, ICommandBuilder> scriptsAfter)
        {
            await Run(
                new Dictionary<FileStream, ICommandBuilder>() { { scriptBefore, commandBuilderBefore } },
                packageManagers,
                scriptsAfter);
        }

        public async Task Run(
            IDictionary<FileStream, ICommandBuilder> scriptsBefore,
            IDictionary<IPackageManager, ICommandBuilder> packageManagers,
            IDictionary<FileStream, ICommandBuilder> scriptsAfter)
        {
            GenerateBeforeScripts(scriptsBefore);
            await GeneratePackageManagerScripts(packageManagers);
            GenerateAfterScripts(scriptsAfter);

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

        private void GenerateBeforeScripts(IDictionary<FileStream, ICommandBuilder> scripts)
        {
            foreach (var script in scripts)
            {
                string fullLocalScriptPath = _fileSystem.Path.Combine(PathResolvers.GetBeforeScriptsPath(_options.RootFilesDirectoryLocation), _fileSystem.Path.GetFileName(script.Key.Name));
                string fullSandboxScriptPath = _fileSystem.Path.Combine(PathResolvers.GetBeforeScriptsPath(_options.RootSandboxFilesDirectoryLocation), _fileSystem.Path.GetFileName(fullLocalScriptPath));

                _fileSystem.File.Copy(_fileSystem.Path.GetFullPath(script.Key.Name), fullLocalScriptPath, true);

                _sandboxConfigurationBuilder.AddCommands(script.Value.Build(fullSandboxScriptPath));
            }
        }

        private async Task GeneratePackageManagerScripts(IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            foreach (var packageManager in packageManagers)
            {
                string scriptName = await packageManager.Key.GenerateScriptFile(PathResolvers.GetPackageManagerScriptsPath(_options.RootFilesDirectoryLocation));
                string fullSandboxScriptName = _fileSystem.Path.Combine(PathResolvers.GetPackageManagerScriptsPath(_options.RootSandboxFilesDirectoryLocation), scriptName);

                _sandboxConfigurationBuilder.AddCommands(packageManager.Value.Build(fullSandboxScriptName));
            }
        }

        private void GenerateAfterScripts(IDictionary<FileStream, ICommandBuilder> scriptsAfter)
        {
            foreach (var script in scriptsAfter)
            {
                string fullLocalScriptPath = _fileSystem.Path.Combine(PathResolvers.GetAfterScriptsPath(_options.RootFilesDirectoryLocation), _fileSystem.Path.GetFileName(script.Key.Name));
                string fullSandboxScriptPath = _fileSystem.Path.Combine(PathResolvers.GetAfterScriptsPath(_options.RootSandboxFilesDirectoryLocation), _fileSystem.Path.GetFileName(fullLocalScriptPath));

                _fileSystem.File.Copy(_fileSystem.Path.GetFullPath(script.Key.Name), fullLocalScriptPath, true);

                _sandboxConfigurationBuilder.AddCommands(script.Value.Build(fullSandboxScriptPath));
            }
        }
    }
}
