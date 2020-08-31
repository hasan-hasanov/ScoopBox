using ScoopBox.SandboxProcesses.ProcessAdapters;
using System;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.ProcessAdapters.ProcessAdapterTests
{
    public class WaitForExitMethodTests
    {
        [Fact]
        public void ShouldCallStandardInputCloseMethod()
        {
            ProcessAdapter processAdapter = new ProcessAdapter("test");

            Assert.Throws<InvalidOperationException>(() => processAdapter.WaitForExit());
        }
    }
}
