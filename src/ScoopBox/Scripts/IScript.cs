using System.IO;

namespace ScoopBox.Scripts
{
    public interface IScript
    {
        FileSystemInfo ScriptFile { get; }
    }
}
