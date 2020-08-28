using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly ISandboxProcess _scoopBoxProcess;
        private readonly ISandboxConfigurationBuilder _sandboxScriptBuilder;
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

        public Sandbox(ISandboxConfigurationBuilder sandboxScriptBuilder)
            : this(new SandboxCmdProcess(), sandboxScriptBuilder, new ScoopPackageManager())
        {
        }

        public Sandbox(ISandboxProcess scoopBoxProcess, ISandboxConfigurationBuilder sandboxScriptBuilder, IPackageManager packageManager)
        {
            _scoopBoxProcess = scoopBoxProcess ?? throw new ArgumentNullException(nameof(scoopBoxProcess));
            _sandboxScriptBuilder = sandboxScriptBuilder ?? throw new ArgumentNullException(nameof(sandboxScriptBuilder));
            _packageManager = packageManager ?? throw new ArgumentNullException(nameof(packageManager));
        }

        public async Task Run()
        {
            await _scoopBoxProcess.Start();
        }

        public Task Run(IEnumerable<string> applications)
        {
            throw new System.NotImplementedException();
        }

        public Task Run(FileStream scriptBefore, IEnumerable<string> applications)
        {
            throw new System.NotImplementedException();
        }

        public Task Run(FileStream scriptBefore, IEnumerable<string> applications, FileStream scriptAfter)
        {
            throw new System.NotImplementedException();
        }
    }
}
