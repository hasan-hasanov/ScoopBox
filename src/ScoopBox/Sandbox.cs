using ScoopBox.PackageManager;
using ScoopBox.SandboxConfigurations;
using ScoopBox.SandboxProcesses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly ISandboxProcess _scoopBoxProcess;
        private readonly ISandboxConfigurationBuilder _sandboxConfigurationBuilder;
        private readonly IPackageManager _packageManager;

        public Sandbox()
            : this(new SandboxCmdProcess(), new SandboxConfigurationBuilder(new SandboxConfigurationOptions()), new ScoopPackageManager())
        {
        }

        public Sandbox(SandboxConfigurationOptions options)
            : this(new SandboxCmdProcess(), new SandboxConfigurationBuilder(options), new ScoopPackageManager())
        {
        }

        public Sandbox(ISandboxProcess scoopBoxProcess)
            : this(new SandboxCmdProcess(), new SandboxConfigurationBuilder(new SandboxConfigurationOptions()), new ScoopPackageManager())
        {
        }

        public Sandbox(IPackageManager packageManager)
            : this(new SandboxCmdProcess(), new SandboxConfigurationBuilder(new SandboxConfigurationOptions()), packageManager)
        {
        }

        public Sandbox(ISandboxConfigurationBuilder sandboxConfigurationBuilder)
            : this(new SandboxCmdProcess(), sandboxConfigurationBuilder, new ScoopPackageManager())
        {
        }

        public Sandbox(ISandboxProcess scoopBoxProcess, ISandboxConfigurationBuilder sandboxConfigurationBuilder, IPackageManager packageManager)
        {
            _scoopBoxProcess = scoopBoxProcess ?? throw new ArgumentNullException(nameof(scoopBoxProcess));
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder ?? throw new ArgumentNullException(nameof(sandboxConfigurationBuilder));
            _packageManager = packageManager ?? throw new ArgumentNullException(nameof(packageManager));
        }

        public async Task Run()
        {
            string sandboxConfiguration = _sandboxConfigurationBuilder.Build();
            await GenerateConfigurationFile(sandboxConfiguration);

            await _scoopBoxProcess.StartAsync();
        }

        public async Task Run(FileStream script)
        {
            await Run(new List<FileStream>() { script });
        }

        public Task Run(IEnumerable<FileStream> scripts)
        {
            throw new NotImplementedException();
        }

        public Task Run(IEnumerable<string> applications)
        {
            throw new System.NotImplementedException();
        }

        public async Task Run(FileStream scriptBefore, IEnumerable<string> applications)
        {
            await Run(new List<FileStream>() { scriptBefore }, applications);
        }

        public Task Run(IEnumerable<FileStream> scriptsBefore, IEnumerable<string> applications)
        {
            throw new NotImplementedException();
        }

        public async Task Run(FileStream scriptBefore, IEnumerable<string> applications, FileStream scriptAfter)
        {
            await Run(new List<FileStream>() { scriptBefore }, applications, new List<FileStream>() { scriptAfter });
        }

        public async Task Run(IEnumerable<FileStream> scriptsBefore, IEnumerable<string> applications, FileStream scriptAfter)
        {
            await Run(scriptsBefore, applications, new List<FileStream>() { scriptAfter });
        }

        public async Task Run(FileStream scriptBefore, IEnumerable<string> applications, IEnumerable<FileStream> scriptsAfter)
        {
            await Run(new List<FileStream>() { scriptBefore }, applications, scriptsAfter);
        }

        public Task Run(IEnumerable<FileStream> scriptsBefore, IEnumerable<string> applications, IEnumerable<FileStream> scriptsAfter)
        {
            throw new NotImplementedException();
        }

        private async Task GenerateConfigurationFile(string configuration)
        {
            using (StreamWriter writer = File.CreateText(Constants.SandboxConfigurationFileLocation))
            {
                await writer.WriteAsync(configuration);
            }
        }
    }
}
