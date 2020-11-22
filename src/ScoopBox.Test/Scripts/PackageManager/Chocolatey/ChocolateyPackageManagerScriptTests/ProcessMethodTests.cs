using ScoopBox.Scripts.PackageManagers.Chocolatey;
using ScoopBox.Translators.Powershell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.Scripts.PackageManager.Chocolatey.ChocolateyPackageManagerScriptTests
{
    public class ProcessMethodTests
    {
        [Fact]
        public async Task ShouldGenerateCorrectPackageManagerScriptContent()
        {
            IOptions options = new Options();

            IEnumerable<string> applications = new List<string>() { "git", "curl" };
            StringBuilder sbChocolateyPackageManagerBuilder = new StringBuilder();
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (file, content, token) =>
            {
                return Task.CompletedTask;
            };

            ChocolateyPackageManagerScript chocolateyPackageManager = new ChocolateyPackageManagerScript(
                applications,
                new PowershellTranslator(),
                "testScript.ps1",
                sbChocolateyPackageManagerBuilder,
                writeAllBytesAsync);

            await chocolateyPackageManager.Process(options, CancellationToken.None);

            string expected = @"Write-Host Start executing chocolatey package manager
Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1')
choco feature enable -n allowGlobalConfirmation
choco install git curl
Write-Host Finished executing chocolatey package manager
";
            string actual = sbChocolateyPackageManagerBuilder.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldGenerateCorrectPackageManagerScriptPath()
        {
            IOptions options = new Options();
            string scriptName = "testScript.ps1";
            string actual = string.Empty;

            IEnumerable<string> applications = new List<string>() { "git", "curl" };
            StringBuilder sbChocolateyPackageManagerBuilder = new StringBuilder();
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (file, content, token) =>
            {
                actual = file;
                return Task.CompletedTask;
            };

            ChocolateyPackageManagerScript chocolateyPackageManager = new ChocolateyPackageManagerScript(
                applications,
                new PowershellTranslator(),
                scriptName,
                sbChocolateyPackageManagerBuilder,
                writeAllBytesAsync);

            await chocolateyPackageManager.Process(options, CancellationToken.None);

            string expected = Path.Combine(options.RootFilesDirectoryLocation, scriptName);

            Assert.Equal(expected, actual);
        }
    }
}
