using ScoopBox.PackageManager;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.CommandBuilders
{
    public interface ICommandBuilder
    {
        Task<IEnumerable<string>> Build(FileStream file, string rootScriptFilesLocation, string rootSandboxScriptFilesLocation);

        Task<IEnumerable<string>> Build(IPackageManager packageManager, string rootScriptFilesLocation, string rootSandboxScriptFilesLocation);

        Task<IEnumerable<string>> Build(string literalScript);
    }
}
