using ScoopBox.SandboxProcesses.ProcessAdapters;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.ProcessAdapters.ProcessAdapterTests
{
    public class StandardInputFlushAsyncMethodTests
    {
        [Fact]
        public async Task ShouldCallStandardInputFlushAsync()
        {
            ProcessAdapter processAdapter = new ProcessAdapter("test");

            await Assert.ThrowsAsync<InvalidOperationException>(() => processAdapter.StandardInputFlushAsync());
        }
    }
}
