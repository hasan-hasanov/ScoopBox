using System.IO;

namespace ScoopBox.Translators
{
    public interface IPowershellTranslator
    {
        string Translate(FileSystemInfo file, IOptions options);
    }
}
