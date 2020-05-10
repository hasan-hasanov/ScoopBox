using ScoopBox.Sandbox.Abstract;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.IO;
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

        public async Task Generate(ScoopBoxOptions scoopBoxOptions)
        {
            string script = this.sandboxScriptBuilder.Build(scoopBoxOptions);
            using (StreamWriter writer = File.CreateText($@"{scoopBoxOptions.UserFilesPath}\sandbox.wsb"))
            {
                await writer.WriteAsync(script);
            }
        }
    }
}
