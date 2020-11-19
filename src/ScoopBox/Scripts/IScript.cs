using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.Scripts
{
    public interface IScript
    {
        FileSystemInfo ScriptFile { get; set; }

        Task GenerateScript();
    }
}
