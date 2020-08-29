using System.Collections.Generic;

namespace ScoopBox.PackageManager
{
    public interface IPackageManager
    {
        IEnumerable<string> Suggestions(string characters);

        string BuildScript(IEnumerable<string> applications);
    }
}
