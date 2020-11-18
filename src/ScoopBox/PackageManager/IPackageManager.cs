using ScoopBox.Scripts;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.PackageManager
{
    public interface IPackageManager : IScript
    {
        string PackageManagerScriptName { get; }

        IEnumerable<string> Applications { get; }

        Task<FileSystemInfo> GenerateScriptFile(string location);
    }
}
