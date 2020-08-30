using ScoopBox.CommandBuilders;
using ScoopBox.PackageManager;
using ScoopBox.SandboxConfigurations;
using ScoopBox.SandboxProcesses;
using ScoopBox.SandboxProcesses.Cmd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly IOptions _options;
        private readonly ISandboxProcess _sandboxProcess;
        private readonly ISandboxConfigurationBuilder _sandboxConfigurationBuilder;

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
        {
            _options = options;
            _sandboxProcess = sandboxProcess ?? throw new ArgumentNullException(nameof(sandboxProcess));
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder ?? throw new ArgumentNullException(nameof(sandboxConfigurationBuilder));

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
            Directory.CreateDirectory(_options.RootFilesDirectoryLocation);
            DirectoryInfo di = new DirectoryInfo(_options.RootFilesDirectoryLocation);

            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }

            Directory.CreateDirectory($"{Path.Combine(_options.RootFilesDirectoryLocation, "BeforeScripts")}");
            Directory.CreateDirectory($"{Path.Combine(_options.RootFilesDirectoryLocation, "AfterScripts")}");
            Directory.CreateDirectory($"{Path.Combine(_options.RootFilesDirectoryLocation, "PackageManagerScripts")}");
        }

        private void GenerateBeforeScripts(IDictionary<FileStream, ICommandBuilder> scripts)
        {
            foreach (var script in scripts)
            {
                string fullLocalScriptPath = Path.Combine(PathResolvers.GetBeforeScriptsPath(_options.RootFilesDirectoryLocation), Path.GetFileName(script.Key.Name));
                string fullSandboxScriptPath = Path.Combine(PathResolvers.GetBeforeScriptsPath(_options.RootSandboxFilesDirectoryLocation), Path.GetFileName(fullLocalScriptPath));

                File.Copy(Path.GetFullPath(script.Key.Name), fullLocalScriptPath, true);

                _sandboxConfigurationBuilder.AddCommands(script.Value.Build(fullSandboxScriptPath));
            }
        }

        private async Task GeneratePackageManagerScripts(IDictionary<IPackageManager, ICommandBuilder> packageManagers)
        {
            foreach (var packageManager in packageManagers)
            {
                string scriptName = await packageManager.Key.GenerateScriptFile(PathResolvers.GetPackageManagerScriptsPath(_options.RootFilesDirectoryLocation));
                string fullSandboxScriptName = Path.Combine(PathResolvers.GetPackageManagerScriptsPath(_options.RootSandboxFilesDirectoryLocation), scriptName);

                _sandboxConfigurationBuilder.AddCommands(packageManager.Value.Build(fullSandboxScriptName));
            }
        }

        private void GenerateAfterScripts(IDictionary<FileStream, ICommandBuilder> scriptsAfter)
        {
            foreach (var script in scriptsAfter)
            {
                string fullLocalScriptPath = Path.Combine(PathResolvers.GetAfterScriptsPath(_options.RootFilesDirectoryLocation), Path.GetFileName(script.Key.Name));
                string fullSandboxScriptPath = Path.Combine(PathResolvers.GetAfterScriptsPath(_options.RootSandboxFilesDirectoryLocation), Path.GetFileName(fullLocalScriptPath));

                File.Copy(Path.GetFullPath(script.Key.Name), fullLocalScriptPath, true);

                _sandboxConfigurationBuilder.AddCommands(script.Value.Build(fullSandboxScriptPath));
            }
        }
    }
}
