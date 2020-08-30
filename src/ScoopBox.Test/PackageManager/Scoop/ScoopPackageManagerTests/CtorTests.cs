using ScoopBox.PackageManager.Scoop;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace ScoopBox.Test.PackageManager.Scoop.ScoopPackageManagerTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeDefaultPackageManagerScriptNameWithApplications()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };

            string expected = $"{ nameof(ScoopPackageManager)}.ps1";

            ScoopPackageManager scoopPackageManager = new ScoopPackageManager(applications);

            Assert.True(scoopPackageManager.PackageManagerScriptName == expected);
        }

        [Fact]
        public void ShouldInitializePackageManagerScriptNameWithApplications()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };
            string packageManagerName = "scoop.ps1";

            string expected = packageManagerName; ;

            ScoopPackageManager scoopPackageManager = new ScoopPackageManager(packageManagerName, applications);

            Assert.True(scoopPackageManager.PackageManagerScriptName == expected);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutApplications()
        {
            IEnumerable<string> applications = null;
            string packageManagerName = "scoop.ps1";
            MockFileSystem mockFileSystem = new MockFileSystem();

            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManager(packageManagerName, applications, mockFileSystem.FileSystem));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutScriptName()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };
            string packageManagerName = null;
            MockFileSystem mockFileSystem = new MockFileSystem();

            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManager(packageManagerName, applications, mockFileSystem.FileSystem));
        }
    }
}
