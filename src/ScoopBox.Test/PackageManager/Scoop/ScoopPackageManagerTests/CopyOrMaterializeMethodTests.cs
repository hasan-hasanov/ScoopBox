using ScoopBox.Scripts.PackageManagers.Scoop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.PackageManager.Scoop.ScoopPackageManagerTests
{
    public class CopyOrMaterializeMethodTests
    {
        [Fact]
        public async Task ShouldGenerateCorrectPackageManagerScriptContent()
        {
            IOptions options = new Options();

            IEnumerable<string> applications = new List<string>() { "git", "curl" };
            StringBuilder sbScoopPackageManagerBuilder = new StringBuilder();
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (file, content, token) =>
            {
                return Task.CompletedTask;
            };

            ScoopPackageManager scoopPackageManager = new ScoopPackageManager(
                applications,
                "testScript.ps1",
                sbScoopPackageManagerBuilder,
                writeAllBytesAsync);

            await scoopPackageManager.CopyOrMaterialize(options, CancellationToken.None);

            string expected = @"Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')
scoop install git
scoop bucket add extras
scoop install git curl
";
            string actual = sbScoopPackageManagerBuilder.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldGenerateCorrectPackageManagerScriptPath()
        {
            IOptions options = new Options();
            string scriptName = "testScript.ps1";
            string actual = string.Empty;

            IEnumerable<string> applications = new List<string>() { "git", "curl" };
            StringBuilder sbScoopPackageManagerBuilder = new StringBuilder();
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (file, content, token) =>
            {
                actual = file;
                return Task.CompletedTask;
            };

            ScoopPackageManager scoopPackageManager = new ScoopPackageManager(
                applications,
                scriptName,
                sbScoopPackageManagerBuilder,
                writeAllBytesAsync);

            await scoopPackageManager.CopyOrMaterialize(options, CancellationToken.None);

            string expected = Path.Combine(options.RootFilesDirectoryLocation, scriptName);

            Assert.Equal(expected, actual);
        }
    }
}
