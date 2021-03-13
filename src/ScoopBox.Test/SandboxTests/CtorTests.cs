using System;
using System.Collections.Generic;
using System.Threading;
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
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory = (scripts, translator, name, options) =>
            {
                Func<string, byte[], CancellationToken, Task> writeAllBytes = (path, content, token) => Task.CompletedTask;

                return Task.FromResult(new LiteralScript(scripts, translator, name, writeAllBytes));
            };

            new Sandbox(options, configurationBuilder, createDirectory, deleteFiles, deleteDirectories, startProcess, literalScriptFactory);

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
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory = (scripts, translator, name, options) =>
            {
                Func<string, byte[], CancellationToken, Task> writeAllBytes = (path, content, token) => Task.CompletedTask;

                return Task.FromResult(new LiteralScript(scripts, translator, name, writeAllBytes));
            };

            Assert.Throws<ArgumentNullException>(() => new Sandbox(null, configurationBuilder, createDirectory, deleteFiles, deleteDirectories, startProcess, literalScriptFactory));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutConfigurationBuilder()
        {
            IOptions options = new Options();
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory = (scripts, translator, name, options) =>
            {
                Func<string, byte[], CancellationToken, Task> writeAllBytes = (path, content, token) => Task.CompletedTask;

                return Task.FromResult(new LiteralScript(scripts, translator, name, writeAllBytes));
            };

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, null, createDirectory, deleteFiles, deleteDirectories, startProcess, literalScriptFactory));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutCreateDirectory()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory = (scripts, translator, name, options) =>
            {
                Func<string, byte[], CancellationToken, Task> writeAllBytes = (path, content, token) => Task.CompletedTask;

                return Task.FromResult(new LiteralScript(scripts, translator, name, writeAllBytes));
            };

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, null, deleteFiles, deleteDirectories, startProcess, literalScriptFactory));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutEnumerateFiles()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> createDirectory = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory = (scripts, translator, name, options) =>
            {
                Func<string, byte[], CancellationToken, Task> writeAllBytes = (path, content, token) => Task.CompletedTask;

                return Task.FromResult(new LiteralScript(scripts, translator, name, writeAllBytes));
            };

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, createDirectory, null, deleteDirectories, startProcess, literalScriptFactory));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutEnumerateDirectories()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory = (scripts, translator, name, options) =>
            {
                Func<string, byte[], CancellationToken, Task> writeAllBytes = (path, content, token) => Task.CompletedTask;

                return Task.FromResult(new LiteralScript(scripts, translator, name, writeAllBytes));
            };

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, createDirectory, deleteFiles, null, startProcess, literalScriptFactory));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutStartProcess()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory = (scripts, translator, name, options) =>
            {
                Func<string, byte[], CancellationToken, Task> writeAllBytes = (path, content, token) => Task.CompletedTask;

                return Task.FromResult(new LiteralScript(scripts, translator, name, writeAllBytes));
            };

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, createDirectory, deleteFiles, deleteDirectories, null, literalScriptFactory));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutLiteralScriptFactory()
        {
            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(options);
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;

            Assert.Throws<ArgumentNullException>(() => new Sandbox(options, configurationBuilder, createDirectory, deleteFiles, deleteDirectories, startProcess, null));
        }
    }
}
