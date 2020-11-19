using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.Scripts
{
    public class Script : IScript
    {
        public Script(FileSystemInfo file)
        {
            ScriptFile = file;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public Task GenerateScript()
        {
            throw new System.NotImplementedException();
        }
    }
}
