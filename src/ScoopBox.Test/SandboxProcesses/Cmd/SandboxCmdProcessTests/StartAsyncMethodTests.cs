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
            mockProcessAdapter.Setup(p => p.Start(It.IsAny<string>())).Returns(true);

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            mockProcessAdapter.Verify(p => p.Start(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCallStandartInputWriteLineMethodSucessfully()
        {
            string rootFileDirectoryLocation = "C:/temp/";
            string configurationName = "sandbox.wsb";

            Mock<IProcessAdapter> mockProcessAdapter = new Mock<IProcessAdapter>();
            mockProcessAdapter.Setup(p => p.StandartInputWriteLine(It.IsAny<string>()));

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            mockProcessAdapter.Verify(p => p.StandartInputWriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCallWaitForExitMethodSucessfully()
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
            mockProcessAdapter.Setup(p => p.StandartInputWriteLine(It.IsAny<string>()))
                .Callback<string>(input => actual = input)
                .Returns(Task.CompletedTask);

            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess(rootFileDirectoryLocation, configurationName, mockProcessAdapter.Object);
            await sandboxCmdProcess.StartAsync();

            Assert.True(expected == actual);
        }
    }
}
