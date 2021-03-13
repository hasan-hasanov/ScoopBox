using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.PackageManager.Scoop.ScoopPackageManagerTests
{
    public class ProcessMethodTests
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

            ScoopPackageManagerScript scoopPackageManager = new ScoopPackageManagerScript(
                applications,
                new PowershellTranslator(),
                "testScript.ps1",
                sbScoopPackageManagerBuilder,
                writeAllBytesAsync);

            await scoopPackageManager.Process(options, CancellationToken.None);

            string expected = @"Write-Host Start executing scoop package manager
Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')
scoop install git
scoop bucket add extras
scoop bucket add nerd-fonts
scoop bucket add nirsoft
scoop bucket add java
scoop bucket add jetbrains
scoop bucket add nonportable
scoop bucket add php
scoop install git curl
Write-Host Finished executing scoop package manager
";
            string actual = sbScoopPackageManagerBuilder.ToString();

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
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

            ScoopPackageManagerScript scoopPackageManager = new ScoopPackageManagerScript(
                applications,
                new PowershellTranslator(),
                scriptName,
                sbScoopPackageManagerBuilder,
                writeAllBytesAsync);

            await scoopPackageManager.Process(options, CancellationToken.None);

            string expected = Path.Combine(options.RootFilesDirectoryLocation, scriptName);

            Assert.Equal(expected, actual);
        }
    }
}
