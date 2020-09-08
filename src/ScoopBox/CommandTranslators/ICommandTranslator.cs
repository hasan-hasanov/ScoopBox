using System.Collections.Generic;
using System.IO;

namespace ScoopBox.CommandTranslators
{
    public interface ICommandTranslator
    {
        IEnumerable<string> Translate(FileSystemInfo file, string rootSandboxScriptFilesLocation);
    }
}
