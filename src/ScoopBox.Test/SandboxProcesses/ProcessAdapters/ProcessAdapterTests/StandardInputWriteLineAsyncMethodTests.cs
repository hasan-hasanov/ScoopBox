using ScoopBox.SandboxProcesses.ProcessAdapters;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.ProcessAdapters.ProcessAdapterTests
{
    public class StandardInputWriteLineAsyncMethodTests
    {
        [Fact]
        public async Task ShouldCallStandardInputWriteLineAsync()
        {
            ProcessAdapter processAdapter = new ProcessAdapter("test");

            await Assert.ThrowsAsync<InvalidOperationException>(() => processAdapter.StandardInputWriteLineAsync("input text"));
        }
    }
}
