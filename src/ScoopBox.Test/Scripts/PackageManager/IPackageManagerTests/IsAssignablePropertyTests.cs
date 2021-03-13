using System.Collections.Generic;
using Xunit;

namespace ScoopBox.Test.Scripts.PackageManager.IPackageManagerTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsOptionsAssignableToIOptions()
        {
            IPackageManagerScript packageManager = new ScoopPackageManagerScript(new List<string>() { "git" }, new PowershellTranslator());

            Assert.IsAssignableFrom<IPackageManagerScript>(packageManager);
        }
    }
}
