using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.ScriptGenerator
{
    public interface IScriptGenerator
    {
        string ScriptName { get; }

        Task<FileSystemInfo> Generate(string path, string content);
    }
}
