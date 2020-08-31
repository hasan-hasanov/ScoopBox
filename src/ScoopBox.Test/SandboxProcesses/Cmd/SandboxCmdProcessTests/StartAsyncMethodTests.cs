using Moq;
using ScoopBox.SandboxProcesses.Cmd;
using ScoopBox.SandboxProcesses.ProcessAdapters;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.Cmd.SandboxCmdProcessTests
{
    public class StartAsyncMethodTests
    {
        [Fact]
        public async Task ShouldCallStartAsyncMethodSucessfully()
        {
            string rootFileDirectoryLocation = "C:/temp/";
            string configurationName = "sandbox.wsb";

            Mock<IProcessAdapter> mockProcessAdapter = new Mock<IProcessAdapter>();
            mockProcessAdapter.Setup(p => p.Start()).Returns(true);

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            mockProcessAdapter.Verify(p => p.Start(), Times.Once);
        }

        [Fact]
        public async Task ShouldCallStandardInputWriteLineAsyncSucessfully()
        {
            string rootFileDirectoryLocation = "C:/temp/";
            string configurationName = "sandbox.wsb";

            Mock<IProcessAdapter> mockProcessAdapter = new Mock<IProcessAdapter>();
            mockProcessAdapter.Setup(p => p.StandardInputWriteLineAsync(It.IsAny<string>()));

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            mockProcessAdapter.Verify(p => p.StandardInputWriteLineAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCallStandardInputFlushAsyncSucessfully()
        {
            string rootFileDirectoryLocation = "C:/temp/";
            string configurationName = "sandbox.wsb";

            Mock<IProcessAdapter> mockProcessAdapter = new Mock<IProcessAdapter>();
            mockProcessAdapter.Setup(p => p.StandardInputFlushAsync());

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            mockProcessAdapter.Verify(p => p.StandardInputFlushAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldCallStandardInputCloseSucessfully()
        {
            string rootFileDirectoryLocation = "C:/temp/";
            string configurationName = "sandbox.wsb";

            Mock<IProcessAdapter> mockProcessAdapter = new Mock<IProcessAdapter>();
            mockProcessAdapter.Setup(p => p.StandardInputClose());

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            mockProcessAdapter.Verify(p => p.StandardInputClose(), Times.Once);
        }

        [Fact]
        public async Task ShouldCallWaitForExitSucessfully()
        {
            string rootFileDirectoryLocation = "C:/temp/";
            string configurationName = "sandbox.wsb";

            Mock<IProcessAdapter> mockProcessAdapter = new Mock<IProcessAdapter>();
            mockProcessAdapter.Setup(p => p.WaitForExit());

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            mockProcessAdapter.Verify(p => p.WaitForExit(), Times.Once);
        }

        [Fact]
        public async Task ShouldWriteToStandartInputSucessfully()
        {
            string rootFileDirectoryLocation = "C:/temp/";
            string configurationName = "sandbox.wsb";

            string expected = $"\"{Path.Combine(rootFileDirectoryLocation, configurationName)}\"";
            string actual = string.Empty;

            Mock<IProcessAdapter> mockProcessAdapter = new Mock<IProcessAdapter>();
            mockProcessAdapter.Setup(p => p.StandardInputWriteLineAsync(It.IsAny<string>()))
                .Callback<string>(input => actual = input)
                .Returns(Task.CompletedTask);

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            Assert.True(expected == actual);
        }
    }
}
