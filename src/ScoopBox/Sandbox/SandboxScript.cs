using ScoopBox.Sandbox.Abstract;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.Sandbox
{
    public class SandboxScript : ISandboxScript
    {
        private readonly ISandboxScriptBuilder sandboxScriptBuilder;

        public SandboxScript(ISandboxScriptBuilder sandboxScriptBuilder)
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
