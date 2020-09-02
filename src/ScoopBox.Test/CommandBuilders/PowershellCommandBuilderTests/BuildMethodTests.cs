using Moq;
using Newtonsoft.Json;
using ScoopBox.CommandBuilders;
using ScoopBox.PackageManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.CommandBuilders.PowershellCommandBuilderTests
{
    public class BuildMethodTests
    {
        [Fact]
        public async Task ShouldCopyFileToCorrectLocationAndBuildSriptWithBothArgs()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";
            string fullScriptFilePath = @"C://Users/test.ps1";
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };

            Mock<IFileSystem> fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(f => f.File.Copy(It.IsAny<string>(), It.IsAny<string>()));

            Mock<FileSystemInfo> fileSystemInfoMock = new Mock<FileSystemInfo>();
            fileSystemInfoMock.SetupGet(f => f.Name).Returns(Path.GetFileName(fullScriptFilePath));
            fileSystemInfoMock.SetupGet(f => f.FullName).Returns(fullScriptFilePath);

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe -ExecutionPolicy Bypass -File {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}",
                $"powershell.exe {string.Join(" ", argsBefore)} {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))} {string.Join(" ", argsAfter)}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(argsBefore, argsAfter, fileSystemMock.Object);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(fileSystemInfoMock.Object, rootScriptFilesLocation, rootSandboxScriptFilesLocation);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldCopyFileToCorrectLocationAndBuildSriptWithBeforeArgs()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";
            string fullScriptFilePath = @"C://Users/test.ps1";
            string[] argsBefore = new string[] { "-t", "Test" };

            Mock<IFileSystem> fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(f => f.File.Copy(It.IsAny<string>(), It.IsAny<string>()));

            Mock<FileSystemInfo> fileSystemInfoMock = new Mock<FileSystemInfo>();
            fileSystemInfoMock.SetupGet(f => f.Name).Returns(Path.GetFileName(fullScriptFilePath));
            fileSystemInfoMock.SetupGet(f => f.FullName).Returns(fullScriptFilePath);

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe -ExecutionPolicy Bypass -File {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}",
                $"powershell.exe {string.Join(" ", argsBefore)} {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(argsBefore, null, fileSystemMock.Object);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(fileSystemInfoMock.Object, rootScriptFilesLocation, rootSandboxScriptFilesLocation);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldCopyFileToCorrectLocationAndBuildSriptWithAfterArgs()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";
            string fullScriptFilePath = @"C://Users/test.ps1";
            string[] argsAfter = new string[] { "-r", "Rest" };

            Mock<IFileSystem> fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(f => f.File.Copy(It.IsAny<string>(), It.IsAny<string>()));

            Mock<FileSystemInfo> fileSystemInfoMock = new Mock<FileSystemInfo>();
            fileSystemInfoMock.SetupGet(f => f.Name).Returns(Path.GetFileName(fullScriptFilePath));
            fileSystemInfoMock.SetupGet(f => f.FullName).Returns(fullScriptFilePath);

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe -ExecutionPolicy Bypass -File {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}",
                $"powershell.exe {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))} {string.Join(" ", argsAfter)}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(null, argsAfter, fileSystemMock.Object);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(fileSystemInfoMock.Object, rootScriptFilesLocation, rootSandboxScriptFilesLocation);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldCopyFileToCorrectLocationAndBuildSriptWithoutArgs()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";
            string fullScriptFilePath = @"C://Users/test.ps1";

            Mock<IFileSystem> fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(f => f.File.Copy(It.IsAny<string>(), It.IsAny<string>()));

            Mock<FileSystemInfo> fileSystemInfoMock = new Mock<FileSystemInfo>();
            fileSystemInfoMock.SetupGet(f => f.Name).Returns(Path.GetFileName(fullScriptFilePath));
            fileSystemInfoMock.SetupGet(f => f.FullName).Returns(fullScriptFilePath);

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe -ExecutionPolicy Bypass -File {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}",
                $"powershell.exe {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(null, null, fileSystemMock.Object);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(fileSystemInfoMock.Object, rootScriptFilesLocation, rootSandboxScriptFilesLocation);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task CopyFileShouldThrowExceptionWithoutFile()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            await Assert.ThrowsAsync<ArgumentNullException>(() => powershellCommandBuilder.Build(file: null, rootScriptFilesLocation, rootSandboxScriptFilesLocation));
        }

        [Fact]
        public async Task CopyFileShouldThrowExceptionWithoutRootPath()
        {
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";

            Mock<IFileSystem> fileSystemMock = new Mock<IFileSystem>();
            Mock<FileSystemInfo> fileSystemInfoMock = new Mock<FileSystemInfo>();

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            await Assert.ThrowsAsync<ArgumentNullException>(() => powershellCommandBuilder.Build(fileSystemInfoMock.Object, null, rootSandboxScriptFilesLocation));
        }

        [Fact]
        public async Task CopyFileShouldThrowExceptionWithoutSandboxRootPath()
        {
            string rootScriptFilesLocation = @"C://Users/Root";

            Mock<FileSystemInfo> fileSystemInfoMock = new Mock<FileSystemInfo>();

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            await Assert.ThrowsAsync<ArgumentNullException>(() => powershellCommandBuilder.Build(fileSystemInfoMock.Object, rootScriptFilesLocation, null));
        }

        [Fact]
        public async Task ShouldGeneratePackageManagerScriptToCorrectLocationAndBuildSriptWithBothArgs()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";
            string fullScriptFilePath = @"C://Users/Root/PackageManagerScripts/ScoopPackageManager.ps1";
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };

            MockFileSystem mockFileSystem = new MockFileSystem();
            Mock<IPackageManager> packageManagerMock = new Mock<IPackageManager>();
            packageManagerMock.Setup(p => p.GenerateScriptFile(It.IsAny<string>())).ReturnsAsync(fullScriptFilePath);

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe -ExecutionPolicy Bypass -File {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}",
                $"powershell.exe {string.Join(" ", argsBefore)} {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))} {string.Join(" ", argsAfter)}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(argsBefore, argsAfter, mockFileSystem);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(packageManagerMock.Object, rootScriptFilesLocation, rootSandboxScriptFilesLocation);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldGeneratePackageManagerScriptToCorrectLocationAndBuildSriptWithBeforeArgs()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";
            string fullScriptFilePath = @"C://Users/Root/PackageManagerScripts/ScoopPackageManager.ps1";
            string[] argsBefore = new string[] { "-t", "Test" };

            MockFileSystem mockFileSystem = new MockFileSystem();
            Mock<IPackageManager> packageManagerMock = new Mock<IPackageManager>();
            packageManagerMock.Setup(p => p.GenerateScriptFile(It.IsAny<string>())).ReturnsAsync(fullScriptFilePath);

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe -ExecutionPolicy Bypass -File {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}",
                $"powershell.exe {string.Join(" ", argsBefore)} {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(argsBefore, null, mockFileSystem);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(packageManagerMock.Object, rootScriptFilesLocation, rootSandboxScriptFilesLocation);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldGeneratePackageManagerScriptToCorrectLocationAndBuildSriptWithAfterArgs()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";
            string fullScriptFilePath = @"C://Users/Root/PackageManagerScripts/ScoopPackageManager.ps1";
            string[] argsAfter = new string[] { "-r", "Rest" };

            MockFileSystem mockFileSystem = new MockFileSystem();
            Mock<IPackageManager> packageManagerMock = new Mock<IPackageManager>();
            packageManagerMock.Setup(p => p.GenerateScriptFile(It.IsAny<string>())).ReturnsAsync(fullScriptFilePath);

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe -ExecutionPolicy Bypass -File {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}",
                $"powershell.exe {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))} {string.Join(" ", argsAfter)}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(null, argsAfter, mockFileSystem);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(packageManagerMock.Object, rootScriptFilesLocation, rootSandboxScriptFilesLocation);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldGeneratePackageManagerScriptToCorrectLocationAndBuildSriptWithoutArgs()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";
            string fullScriptFilePath = @"C://Users/Root/PackageManagerScripts/ScoopPackageManager.ps1";

            MockFileSystem mockFileSystem = new MockFileSystem();
            Mock<IPackageManager> packageManagerMock = new Mock<IPackageManager>();
            packageManagerMock.Setup(p => p.GenerateScriptFile(It.IsAny<string>())).ReturnsAsync(fullScriptFilePath);

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe -ExecutionPolicy Bypass -File {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}",
                $"powershell.exe {Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(fullScriptFilePath))}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(null, null, mockFileSystem);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(packageManagerMock.Object, rootScriptFilesLocation, rootSandboxScriptFilesLocation);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task GeneratePackageManagerScriptShouldThrowExceptionWithoutPackageManager()
        {
            string rootScriptFilesLocation = @"C://Users/Root";
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            await Assert.ThrowsAsync<ArgumentNullException>(() => powershellCommandBuilder.Build(packageManager: null, rootScriptFilesLocation, rootSandboxScriptFilesLocation));
        }

        [Fact]
        public async Task GeneratePackageManagerScriptShouldThrowExceptionWithoutRootPath()
        {
            string rootSandboxScriptFilesLocation = @"C://Users/Sandbox";

            Mock<IFileSystem> fileSystemMock = new Mock<IFileSystem>();
            Mock<IPackageManager> packageManagerMock = new Mock<IPackageManager>();

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            await Assert.ThrowsAsync<ArgumentNullException>(() => powershellCommandBuilder.Build(packageManagerMock.Object, null, rootSandboxScriptFilesLocation));
        }

        [Fact]
        public async Task GeneratePackageManagerScriptShouldThrowExceptionWithoutSandboxRootPath()
        {
            string rootScriptFilesLocation = @"C://Users/Root";

            Mock<IPackageManager> packageManagerMock = new Mock<IPackageManager>();

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            await Assert.ThrowsAsync<ArgumentNullException>(() => powershellCommandBuilder.Build(packageManagerMock.Object, rootScriptFilesLocation, null));
        }

        [Fact]
        public async Task ShouldGenerateLiteralScriptCorrectlyWithBothArgs()
        {
            string literalScript = @"New-Item 'C:\Users\WDAGUtilityAccount\Desktop\TestText.txt'";
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe {string.Join(" ", argsBefore)} {literalScript} {string.Join(" ", argsAfter)}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(argsBefore, argsAfter);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(literalScript);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldGenerateLiteralScriptCorrectlyWithBeforeArgs()
        {
            string literalScript = @"New-Item 'C:\Users\WDAGUtilityAccount\Desktop\TestText.txt'";
            string[] argsBefore = new string[] { "-t", "Test" };

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe {string.Join(" ", argsBefore)} {literalScript}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(argsBefore, null);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(literalScript);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldGenerateLiteralScriptCorrectlyWithAfterArgs()
        {
            string literalScript = @"New-Item 'C:\Users\WDAGUtilityAccount\Desktop\TestText.txt'";
            string[] argsAfter = new string[] { "-r", "Rest" };

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe {literalScript} {string.Join(" ", argsAfter)}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(null, argsAfter);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(literalScript);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task ShouldGenerateLiteralScriptCorrectlyWithoutArgs()
        {
            string literalScript = @"New-Item 'C:\Users\WDAGUtilityAccount\Desktop\TestText.txt'";

            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe {literalScript}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(null, null);
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(literalScript);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public async Task GenerateLiteralScriptShouldNOTThrowExceptionWithoutLiteralString()
        {
            IEnumerable<string> expectedRaw = new List<string>()
            {
                $"powershell.exe"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();
            IEnumerable<string> actualRaw = await powershellCommandBuilder.Build(null);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }
    }
}
