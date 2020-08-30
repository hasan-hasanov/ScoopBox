using ScoopBox.SandboxConfigurations;
using System;
using System.IO.Abstractions;
using System.Linq;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.SandboxConfigurationBuilderTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeCommands()
        {
            IOptions options = new Options();

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);

            Assert.True(sandboxConfigurationBuilder.Commands != null);
        }

        [Fact]
        public void ShouldInitializeEmptyCommands()
        {
            IOptions options = new Options();

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);

            Assert.False(sandboxConfigurationBuilder.Commands.Any());
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutOptions()
        {
            IOptions options = null;

            Assert.Throws<ArgumentNullException>(() => new SandboxConfigurationBuilder(options));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutFileSystem()
        {
            IOptions options = new Options();
            IFileSystem fileSystem = null;

            Assert.Throws<ArgumentNullException>(() => new SandboxConfigurationBuilder(options, fileSystem));
        }
    }
}
