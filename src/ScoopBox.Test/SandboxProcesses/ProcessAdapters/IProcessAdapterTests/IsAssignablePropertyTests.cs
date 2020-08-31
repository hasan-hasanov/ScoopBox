using ScoopBox.SandboxProcesses.ProcessAdapters;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.ProcessAdapters.IProcessAdapterTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsProcessAdapterAssignableToIProcessAdapter()
        {
            ProcessAdapter processAdapter = new ProcessAdapter("test");

            Assert.IsAssignableFrom<IProcessAdapter>(processAdapter);
        }
    }
}
