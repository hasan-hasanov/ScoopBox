using ScoopBox.SandboxProcesses.ProcessAdapters;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.ProcessAdapters.ProcessAdapterTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeProcessAdapterClassSuccessfully()
        {
            ProcessAdapter processAdapter = new ProcessAdapter("test");

            Assert.True(processAdapter != null);
        }
    }
}
