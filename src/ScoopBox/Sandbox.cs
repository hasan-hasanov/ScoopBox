using ScoopBox.PackageManager;
using ScoopBox.SandboxConfigurations;
using ScoopBox.SandboxProcesses;
using ScoopBox.Scripts;
using ScoopBox.Translators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoopBox
{
    public class Sandbox : ISandbox
    {
        private readonly IOptions _options;
        private readonly ISandboxProcess _process;
        private readonly ISandboxConfigurationBuilder _sandboxConfigurationBuilder;

        public Sandbox()
            : this(new Options(), new CmdProcess(), new SandboxConfigurationBuilder(new Options()))
        {

        }

        public Sandbox(IOptions options, ISandboxProcess process, ISandboxConfigurationBuilder sandboxConfigurationBuilder)
        {
            _options = options;
            _process = process;
            _sandboxConfigurationBuilder = sandboxConfigurationBuilder;
        }

        public Task Run(string literalScript)
        {
            throw new System.NotImplementedException();
        }

        public Task Run(List<string> literalScripts)
        {
            throw new System.NotImplementedException();
        }

        public async Task Run(IPackageManager packageManager)
        {
            await Run(packageManager, null);
        }

        public async Task Run(IScript script, IPowershellTranslator translator)
        {
            await Run(new List<Tuple<IScript, IPowershellTranslator>>()
            {
                Tuple.Create(script, translator)
            });
        }

        public async Task Run(List<Tuple<IScript, IPowershellTranslator>> scripts)
        {
            foreach ((IScript script, IPowershellTranslator translator) in scripts)
            {

            }
        }
    }
}
