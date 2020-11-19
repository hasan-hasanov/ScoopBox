using ScoopBox.Scripts;
using System.Collections.Generic;

namespace ScoopBox.PackageManager
{
    public interface IPackageManager : IScript
    {
        IEnumerable<string> Applications { get; }
    }
}
