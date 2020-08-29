using ScoopBox.CommandBuilders;
using ScoopBox.PackageManager;
using ScoopBox.PackageManager.Scoop;
using ScoopBox.SandboxConfigurations;
using ScoopBox.SandboxProcesses;
using ScoopBox.SandboxProcesses.CMD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly IOptions _options;
        private readonly ISandboxProcess _scoopBoxProcess;
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
            _scoopBoxProcess = sandboxProcess ?? throw new ArgumentNullException(nameof(sandboxProcess));
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder ?? throw new ArgumentNullException(nameof(sandboxConfigurationBuilder));
        }

        public async Task Run()
        {
            string sandboxConfiguration = _sandboxConfigurationBuilder.Build();
            await GenerateConfigurationFile(sandboxConfiguration);

            await _scoopBoxProcess.StartAsync();
        }

        public async Task Run(
            FileStream script,
            ICommandBuilder commandBuilder)
        {
            await Run(new Dictionary<FileStream, ICommandBuilder>() { { script, commandBuilder } });
        }

        public async Task Run(IDictionary<FileStream, ICommandBuilder> scripts)
        {
            foreach (var script in scripts)
            {
                string fullSandboxScriptPath = Path.Combine(PathResolvers.GetBeforeScriptsPath(_options.RootFilesDirectoryLocation), Path.GetFileName(script.Key.Name));
                File.Copy(Path.GetFullPath(script.Key.Name), fullSandboxScriptPath);
                _sandboxConfigurationBuilder.AddCommand(script.Value.Build(fullSandboxScriptPath));
            }

            string sandboxConfiguration = _sandboxConfigurationBuilder.Build();
            await GenerateConfigurationFile(sandboxConfiguration);
            await _scoopBoxProcess.StartAsync();
        }

        public Task Run(HashSet<IPackageManager> applications)
        {
            throw new System.NotImplementedException();
        }

        public async Task Run(
            FileStream scriptBefore,
            ICommandBuilder commandBuilder,
            HashSet<IPackageManager> applications)
        {
            await Run(
                new Dictionary<FileStream, ICommandBuilder>() { { scriptBefore, commandBuilder } },
                applications);
        }

        public Task Run(
            IDictionary<FileStream, ICommandBuilder> scriptsBefore,
            HashSet<IPackageManager> applications)
        {
            throw new NotImplementedException();
        }

        public async Task Run(
            FileStream scriptBefore,
            ICommandBuilder commandBuilderBefore,
            HashSet<IPackageManager> applications,
            FileStream scriptAfter,
            ICommandBuilder commandBuilderAfter)
        {
            await Run(
                new Dictionary<FileStream, ICommandBuilder>() { { scriptBefore, commandBuilderBefore } },
                applications,
                new Dictionary<FileStream, ICommandBuilder>() { { scriptAfter, commandBuilderAfter } });
        }

        public async Task Run(
            IDictionary<FileStream, ICommandBuilder> scriptsBefore,
            HashSet<IPackageManager> applications,
            FileStream scriptAfter,
            ICommandBuilder commandBuilderAfter)
        {
            await Run(
                scriptsBefore,
                applications,
                new Dictionary<FileStream, ICommandBuilder>() { { scriptAfter, commandBuilderAfter } });
        }

        public async Task Run(
            FileStream scriptBefore,
            ICommandBuilder commandBuilderBefore,
            HashSet<IPackageManager> applications,
            IDictionary<FileStream, ICommandBuilder> scriptsAfter)
        {
            await Run(
                new Dictionary<FileStream, ICommandBuilder>() { { scriptBefore, commandBuilderBefore } },
                applications,
                scriptsAfter);
        }

        public Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, HashSet<IPackageManager> applications, IDictionary<FileStream, ICommandBuilder> scriptsAfter)
        {
            throw new NotImplementedException();
        }

        private async Task GenerateConfigurationFile(string configuration)
        {
            using (StreamWriter writer = File.CreateText(Path.Combine(_options.RootFilesDirectoryLocation, _options.SandboxConfigurationFileName)))
            {
                await writer.WriteAsync(configuration);
            }
        }
    }
}
