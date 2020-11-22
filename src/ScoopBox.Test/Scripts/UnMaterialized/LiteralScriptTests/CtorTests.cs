using ScoopBox.Scripts.UnMaterialized;
using ScoopBox.Translators;
using ScoopBox.Translators.Powershell;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.Scripts.UnMaterialized.LiteralScriptTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeTranslator()
        {
            IList<string> commands = new List<string>()
            {
                @"Start-Process 'C:\windows\system32\notepad.exe'",
                @"Start-Process .",
            };
            LiteralScript literalScript = new LiteralScript(commands);

            var actual = literalScript.Translator;

            Assert.NotNull(actual);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutCommands()
        {
            IPowershellTranslator translator = new PowershellTranslator();
            string scriptFileName = "MockFileName";
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (path, content, token) => { return Task.CompletedTask; };

            Assert.Throws<ArgumentNullException>(() => new LiteralScript(null, translator, scriptFileName, writeAllBytesAsync));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutTranslator()
        {
            IList<string> commands = new List<string>()
            {
                @"Start-Process 'C:\windows\system32\notepad.exe'",
                @"Start-Process .",
            };
            string scriptFileName = "MockFileName";
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (path, content, token) => { return Task.CompletedTask; };

            Assert.Throws<ArgumentNullException>(() => new LiteralScript(commands, null, scriptFileName, writeAllBytesAsync));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutScriptFileName()
        {
            IList<string> commands = new List<string>()
            {
                @"Start-Process 'C:\windows\system32\notepad.exe'",
                @"Start-Process .",
            };
            IPowershellTranslator translator = new PowershellTranslator();
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (path, content, token) => { return Task.CompletedTask; };

            Assert.Throws<ArgumentNullException>(() => new LiteralScript(commands, translator, null, writeAllBytesAsync));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutWriteAllBytesAsync()
        {
            IList<string> commands = new List<string>()
            {
                @"Start-Process 'C:\windows\system32\notepad.exe'",
                @"Start-Process .",
            };
            IPowershellTranslator translator = new PowershellTranslator();
            string scriptFileName = "MockFileName";
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (path, content, token) => { return Task.CompletedTask; };

            Assert.Throws<ArgumentNullException>(() => new LiteralScript(commands, translator, scriptFileName, null));
        }
    }
}
