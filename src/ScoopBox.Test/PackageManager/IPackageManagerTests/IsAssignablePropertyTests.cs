using ScoopBox.PackageManager;
using ScoopBox.PackageManager.Scoop;
using System.Collections.Generic;
using Xunit;

namespace ScoopBox.Test.PackageManager.IPackageManagerTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsCmdCommandBuilderAssignableToICommandBuilder()
        {
            ScoopPackageManager scoopPackageManager = new ScoopPackageManager(new List<string>() { "git" });

            Assert.IsAssignableFrom<IPackageManager>(scoopPackageManager);
        }
    }
}
