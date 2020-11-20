using ScoopBox.Scripts.PackageManagers;
using ScoopBox.Scripts.PackageManagers.Scoop;
using ScoopBox.Translators.Powershell;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.PackageManager.Scoop.ScoopPackageManagerTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeApplications()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };

            IPackageManagerScript scoopPackageManager = new ScoopPackageManagerScript(applications, new PowershellTranslator());

            string expected = JsonSerializer.Serialize(applications);
            string actual = JsonSerializer.Serialize(scoopPackageManager.Applications);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutApplications()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManagerScript(null, new PowershellTranslator()));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutScriptName()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManagerScript(new List<string>() { "git", "curl", "fiddler" }, null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutSbScoopPackageManagerBuilder()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManagerScript(
                new List<string>() { "git", "curl", "fiddler" },
                new PowershellTranslator(),
                $"{nameof(ScoopPackageManagerScript)}.ps1",
                null,
                (path, content, token) => Task.CompletedTask));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutWriteAllBytesAsync()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManagerScript(
                new List<string>() { "git", "curl", "fiddler" },
                new PowershellTranslator(),
                $"{nameof(ScoopPackageManagerScript)}.ps1",
                new StringBuilder(),
                null));
        }
    }
}
