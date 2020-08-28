using System.Collections;
using System.Collections.Generic;

namespace ScoopBox.PackageManager
{
    public interface IPackageManager
    {
        IEnumerable<string> Suggestions(string characters);

        string GetScript(IEnumerable<string> applications);
    }
}
