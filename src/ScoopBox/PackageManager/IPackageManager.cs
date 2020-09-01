using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoopBox.PackageManager
{
    public interface IPackageManager
    {
        string PackageManagerScriptName { get; }

        IEnumerable<string> Applications { get; }

        Task<string> GenerateScriptFile(string location);
    }
}
