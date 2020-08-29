using System.Collections.Generic;

namespace ScoopBox.PackageManager.Scoop
{
    public class ScoopPackageManager : IPackageManager
    {
        public string BuildScript(IEnumerable<string> applications)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> Autocomplete(string characters)
        {
            throw new System.NotImplementedException();
        }
    }
}
