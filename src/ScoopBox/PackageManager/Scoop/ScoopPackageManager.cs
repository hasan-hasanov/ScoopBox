using System.Collections.Generic;

namespace ScoopBox.PackageManager.Scoop
{
    public class ScoopPackageManager : IPackageManager
    {
        public IEnumerable<string> Applications { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public string GenerateScript()
        {
            throw new System.NotImplementedException();
        }
    }
}
