using ScoopBox.Abstract;
using ScoopBox.Scripts.Abstract;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.Scoop
{
    public class ScriptGenerator : IScriptGenerator
    {
        private readonly IScriptBuilder scriptBuilder;

        public ScriptGenerator(IScriptBuilder scriptBuilder)
        {
            this.scriptBuilder = scriptBuilder;
        }

        public async Task Generate(ScoopBoxOptions scoopBoxOptions)
        {
            string script = this.scriptBuilder.Build(scoopBoxOptions);

            using (StreamWriter writer = File.CreateText($"{scoopBoxOptions.SandboxFilesPath}\\sandbox.ps1"))
            {
                await writer.WriteAsync(script);
            }
        }
    }
}
