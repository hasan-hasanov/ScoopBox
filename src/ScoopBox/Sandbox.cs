using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly Options _options;
        private readonly ISandboxoxProcess _scoopBoxProcess;
        private readonly IPackageManager _packageManager;

        public Sandbox()
            : this(new Options())
        {
        }

        public Sandbox(Options options)
            : this(options, new SandboxCmdProcess())
        {
        }

        public Sandbox(Options options, ISandboxoxProcess scoopBoxProcess)
            : this(options, new SandboxCmdProcess(), new ScoopPackageManager())
        {
        }

        public Sandbox(Options options, IPackageManager packageManager)
            : this(options, new SandboxCmdProcess(), packageManager)
        {
        }

        public Sandbox(Options options, ISandboxoxProcess scoopBoxProcess, IPackageManager packageManager)
        {
            this._options = options ?? throw new ArgumentNullException(nameof(options));
            this._scoopBoxProcess = scoopBoxProcess ?? throw new ArgumentNullException(nameof(scoopBoxProcess));
            this._packageManager = packageManager ?? throw new ArgumentNullException(nameof(packageManager));
        }

        public async Task Run()
        {
            await this._scoopBoxProcess.Start();
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
