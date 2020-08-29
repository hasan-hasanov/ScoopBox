using System.Collections.Generic;

namespace ScoopBox.PackageManager
{
    public interface IPackageManager
    {
        IEnumerable<string> Applications { get; set; }

        string GenerateScript();
    }
}
