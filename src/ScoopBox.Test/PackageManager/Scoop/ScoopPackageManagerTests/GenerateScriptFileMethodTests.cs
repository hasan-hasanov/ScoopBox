using ScoopBox.PackageManager.Scoop;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.PackageManager.Scoop.ScoopPackageManagerTests
{
    public class GenerateScriptFileMethodTests
    {
        [Fact]
        public async Task ShouldGenerateFileWithCorrectScript()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };
            string packageManagerName = "scoop.ps1";
            string location = @"C:\temp\";
            MockFileSystem mockFileSystem = new MockFileSystem();

            StringBuilder expectedRaw = new StringBuilder()
                .AppendLine("Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')")
                .AppendLine("scoop install git")
                .AppendLine("scoop bucket add extras")
                .AppendLine($"scoop install {string.Join(" ", applications)}");

            ScoopPackageManager scoopPackageManager = new ScoopPackageManager(packageManagerName, applications, mockFileSystem);
            await scoopPackageManager.GenerateScriptFile(location);

            MockFileData actualRaw = mockFileSystem.GetFile(Path.Combine(location, packageManagerName));

            string expected = expectedRaw.ToString();
            string actual = actualRaw.TextContents;

            Assert.True(expected == actual);
        }

        [Fact]
        public async Task ShouldGenerateFileWithCorrectExtension()
        {
            IEnumerable<string> applications = new List<string>() { "git", "curl", "fiddler" };
            string packageManagerName = "scoop.txt";
            string location = @"C:\temp\";
            MockFileSystem mockFileSystem = new MockFileSystem();

            ScoopPackageManager scoopPackageManager = new ScoopPackageManager(packageManagerName, applications, mockFileSystem);
            await scoopPackageManager.GenerateScriptFile(location);

            Assert.True(mockFileSystem.FileExists(Path.Combine(location, packageManagerName)));
        }
    }
}
