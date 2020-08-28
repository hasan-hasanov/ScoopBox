using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly ScoopBoxOptions _options;
        private readonly IScoopBoxProcess _scoopBoxProcess;
        private readonly IPackageManager _packageManager;

        public Sandbox()
            : this(new ScoopBoxOptions())
        {
        }

        public Sandbox(ScoopBoxOptions options)
            : this(options, new ScoopBoxCmdProcess())
        {
        }

        public Sandbox(ScoopBoxOptions options, IScoopBoxProcess scoopBoxProcess)
            : this(options, new ScoopBoxCmdProcess(), new ScoopPackageManager())
        {
        }

        public Sandbox(ScoopBoxOptions options, IPackageManager packageManager)
            : this(options, new ScoopBoxCmdProcess(), packageManager)
        {
        }

        public Sandbox(ScoopBoxOptions options, IScoopBoxProcess scoopBoxProcess, IPackageManager packageManager)
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
