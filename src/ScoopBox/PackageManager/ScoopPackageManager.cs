using System.Collections.Generic;

namespace ScoopBox.PackageManager
{
    public class ScoopPackageManager : IPackageManager
    {
        public string GetScript(IEnumerable<string> applications)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> Suggestions(string characters)
        {
            throw new System.NotImplementedException();
        }
    }
}
