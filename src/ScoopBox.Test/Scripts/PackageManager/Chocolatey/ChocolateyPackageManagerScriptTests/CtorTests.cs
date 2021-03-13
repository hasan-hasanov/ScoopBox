using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.Scripts.PackageManager.Chocolatey.ChocolateyPackageManagerScriptTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeWithoutTranslator()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };

            IPackageManagerScript chocolateyPackageManager = new ChocolateyPackageManagerScript(applications);

            string expected = JsonSerializer.Serialize(applications);
            string actual = JsonSerializer.Serialize(chocolateyPackageManager.Applications);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldInitializeApplications()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };

            IPackageManagerScript chocolateyPackageManager = new ChocolateyPackageManagerScript(applications, new PowershellTranslator());

            string expected = JsonSerializer.Serialize(applications);
            string actual = JsonSerializer.Serialize(chocolateyPackageManager.Applications);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldInitializeTranslator()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };
            IPowershellTranslator powershellTranslator = new PowershellTranslator();

            IPackageManagerScript chocolateyPackageManager = new ChocolateyPackageManagerScript(applications, powershellTranslator);

            var expected = powershellTranslator;
            var actual = chocolateyPackageManager.Translator;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutApplications()
        {
            Assert.Throws<ArgumentNullException>(() => new ChocolateyPackageManagerScript(null, new PowershellTranslator()));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutTranslator()
        {
            Assert.Throws<ArgumentNullException>(() => new ChocolateyPackageManagerScript(new List<string>() { "git", "curl", "fiddler" }, null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutScriptName()
        {
            Assert.Throws<ArgumentNullException>(() => new ChocolateyPackageManagerScript(new List<string>() { "git", "curl", "fiddler" }, new PowershellTranslator(), null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutSbChocolateyPackageManagerBuilder()
        {
            Assert.Throws<ArgumentNullException>(() => new ChocolateyPackageManagerScript(
                new List<string>() { "git", "curl", "fiddler" },
                new PowershellTranslator(),
                $"{nameof(ChocolateyPackageManagerScript)}.ps1",
                null,
                (path, content, token) => Task.CompletedTask));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutWriteAllBytesAsync()
        {
            Assert.Throws<ArgumentNullException>(() => new ChocolateyPackageManagerScript(
                new List<string>() { "git", "curl", "fiddler" },
                new PowershellTranslator(),
                $"{nameof(ChocolateyPackageManagerScript)}.ps1",
                new StringBuilder(),
                null));
        }
    }
}
