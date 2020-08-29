using System.Threading.Tasks;

namespace ScoopBox.PackageManager
{
    public interface IPackageManager
    {
        string PackageManagerScriptName { get; }

        Task<string> GenerateScriptFile(string location);
    }
}
