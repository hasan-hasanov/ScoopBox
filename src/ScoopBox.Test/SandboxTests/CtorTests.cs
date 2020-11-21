using ScoopBox.SandboxConfigurations;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.SandboxTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeWithAllParameters()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(new Options());
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;

            new Sandbox(options, configurationBuilder, createDirectory, deleteFiles, deleteDirectories, startProcess);

            Assert.True(true);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutOptions()
        {
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(new Options());
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;

            Assert.Throws<ArgumentNullException>(() => new Sandbox(null, configurationBuilder, createDirectory, deleteFiles, deleteDirectories, startProcess));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutConfigurationBuilder()
        {
            IOptions options = new Options();
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, null, createDirectory, deleteFiles, deleteDirectories, startProcess));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutCreateDirectory()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, null, deleteFiles, deleteDirectories, startProcess));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutEnumerateFiles()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> createDirectory = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, createDirectory, null, deleteDirectories, startProcess));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutEnumerateDirectories()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, createDirectory, deleteFiles, null, startProcess));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutStartProcess()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, createDirectory, deleteFiles, deleteDirectories, null));
        }
    }
}
