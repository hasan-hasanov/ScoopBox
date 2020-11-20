using System.Collections.Generic;

namespace ScoopBox.Scripts.PackageManagers
{
    public interface IPackageManager : IScript
    {
        IEnumerable<string> Applications { get; }
    }
}
