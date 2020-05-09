using ScoopBox.Scoop.Abstract;
using ScoopBox.Scripts.Abstract;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.Scoop
{
    public class ScoopScriptGenerator : IScoopScriptGenerator
    {
        private readonly IScriptBuilder scriptBuilder;

        public ScoopScriptGenerator(IScriptBuilder scriptBuilder)
        {
            this.scriptBuilder = scriptBuilder;
        }

        public async Task Generate(ScoopBoxOptions scoopBoxOptions)
        {
            string script = scriptBuilder.Build(scoopBoxOptions);

            using (StreamWriter writer = File.CreateText($"{scoopBoxOptions.SandboxFilesPath}\\sandbox.ps1"))
            {
                await writer.WriteAsync(script);
            }
        }
    }
}
