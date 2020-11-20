using ScoopBox.PackageManager.Scoop;
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

            ScoopPackageManager scoopPackageManager = new ScoopPackageManager(applications);

            string expected = JsonSerializer.Serialize(applications);
            string actual = JsonSerializer.Serialize(scoopPackageManager.Applications);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutApplications()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManager(null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutScriptName()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManager(new List<string>() { "git", "curl", "fiddler" }, null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutSbScoopPackageManagerBuilder()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManager(
                new List<string>() { "git", "curl", "fiddler" },
                $"{nameof(ScoopPackageManager)}.ps1",
                null,
                (path, content, token) => Task.CompletedTask));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutWriteAllBytesAsync()
        {
            Assert.Throws<ArgumentNullException>(() => new ScoopPackageManager(
                new List<string>() { "git", "curl", "fiddler" },
                $"{nameof(ScoopPackageManager)}.ps1",
                new StringBuilder(),
                null));
        }
    }
}
