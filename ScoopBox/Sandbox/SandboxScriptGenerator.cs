using ScoopBox.ConfigurationEntities;
using ScoopBox.Sandbox.Abstract;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.Threading.Tasks;

namespace ScoopBox.Sandbox
{
    public class SandboxScriptGenerator : ISandboxScriptGenerator
    {
        private readonly ISandboxScriptBuilder sandboxScriptBuilder;

        public SandboxScriptGenerator(ISandboxScriptBuilder sandboxScriptBuilder)
        {
            this.sandboxScriptBuilder = sandboxScriptBuilder;
        }

        public async Task Generate(ScoopBoxOptions options)
        {
            Configuration configuration = new Configuration(options);
            string sandboxScript = this.sandboxScriptBuilder.Build(configuration);
        }
    }
}
