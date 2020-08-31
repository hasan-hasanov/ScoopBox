using ScoopBox.SandboxProcesses.ProcessAdapters;
using System;
using System.ComponentModel;
using Xunit;

namespace ScoopBox.Test.SandboxProcesses.ProcessAdapters.ProcessAdapterTests
{
    public class StartMethodTests
    {
        [Fact]
        public void ShouldCallStartMethod()
        {
            ProcessAdapter processAdapter = new ProcessAdapter("test");

            Assert.Throws<Win32Exception>(() => processAdapter.Start());
        }

        [Fact]
        public void ShouldThrowInvalidOperationExceptionWhenProcessNameIsNull()
        {
            ProcessAdapter processAdapter = new ProcessAdapter(null);

            Assert.Throws<InvalidOperationException>(() => processAdapter.Start());
        }

        [Fact]
        public void ShouldThrowInvalidOperationExceptionWhenProcessNameIsEmpty()
        {
            ProcessAdapter processAdapter = new ProcessAdapter(string.Empty);

            Assert.Throws<InvalidOperationException>(() => processAdapter.Start());
        }
    }
}
