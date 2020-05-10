using ScoopBox.Scoop.Abstract;
using ScoopBox.Scripts.InstallerScripts.Abstract;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.Scoop
{
    public class ScoopScript : IScoopScript
    {
        private readonly IInstallerScriptBuilder scriptBuilder;

        public ScoopScript(IInstallerScriptBuilder scriptBuilder)
        {
            this.scriptBuilder = scriptBuilder;
        }

        public async Task Generate(ScoopBoxOptions scoopBoxOptions)
        {
            string script = scriptBuilder.Build(scoopBoxOptions);

            using (StreamWriter writer = File.CreateText($@"{scoopBoxOptions.UserFilesPath}\sandbox.ps1"))
            {
                await writer.WriteAsync(script);
            }
        }
    }
}
