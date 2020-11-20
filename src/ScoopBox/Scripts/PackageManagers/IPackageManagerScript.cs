using System.Collections.Generic;

namespace ScoopBox.Scripts.PackageManagers
{
    public interface IPackageManagerScript : IScript
    {
        IEnumerable<string> Applications { get; }
    }
}
