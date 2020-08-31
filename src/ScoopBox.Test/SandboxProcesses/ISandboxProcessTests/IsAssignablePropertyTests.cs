using ScoopBox.SandboxProcesses;
using ScoopBox.SandboxProcesses.Cmd;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.ISandboxProcessTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsSandboxProcessAssignableToSandboxCmdProcess()
        {
            SandboxCmdProcess sandboxCmdProcess = new SandboxCmdProcess();

            Assert.IsAssignableFrom<ISandboxProcess>(sandboxCmdProcess);
        }
    }
}
