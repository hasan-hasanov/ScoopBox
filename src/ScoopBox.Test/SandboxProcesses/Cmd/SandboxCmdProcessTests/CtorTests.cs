using ScoopBox.SandboxProcesses.Cmd;
using System;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.Cmd.SandboxCmdProcessTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldCreateInstanceWithoutException()
        {
            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess();
            Assert.True(sandboxCmdProcess != null);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutRootFilesDirectoryLocation()
        {
            string rootFilesDirectoryLocation = null;
            string sandboxConfigurationFileName = "sandbox.wsb";

            Assert.Throws<ArgumentNullException>(() => new SandboxCmdProcess(rootFilesDirectoryLocation, sandboxConfigurationFileName));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutSandboxConfigurationFileName()
        {
            string rootFilesDirectoryLocation = "C:/temp";
            string sandboxConfigurationFileName = null;

            Assert.Throws<ArgumentNullException>(() => new SandboxCmdProcess(rootFilesDirectoryLocation, sandboxConfigurationFileName));
        }
    }
}
