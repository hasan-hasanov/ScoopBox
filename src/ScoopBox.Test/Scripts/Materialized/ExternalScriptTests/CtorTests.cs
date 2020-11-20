using ScoopBox.Scripts.Materialized;
using ScoopBox.Test.Mocks;
using ScoopBox.Translators;
using ScoopBox.Translators.Powershell;
using System;
using System.IO;
using Xunit;

namespace ScoopBox.Test.Scripts.Materialized.ExternalScriptTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeScriptFile()
        {
            string fileName = "MockFileName";
            FileSystemInfo fileSystemInfo = new MockFileSystemInfo(fileName);

            ExternalScript externalScript = new ExternalScript(fileSystemInfo, new PowershellTranslator());

            FileSystemInfo expected = fileSystemInfo;
            FileSystemInfo actual = externalScript.ScriptFile;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldInitializeTranslator()
        {
            string fileName = "MockFileName";
            FileSystemInfo fileSystemInfo = new MockFileSystemInfo(fileName);
            IPowershellTranslator translator = new PowershellTranslator();

            ExternalScript externalScript = new ExternalScript(fileSystemInfo, translator);

            IPowershellTranslator expected = translator;
            IPowershellTranslator actual = externalScript.Translator;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutScriptFile()
        {
            Assert.Throws<ArgumentNullException>(() => new ExternalScript(null, new PowershellTranslator()));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutTranslator()
        {
            Assert.Throws<ArgumentNullException>(() => new ExternalScript(new MockFileSystemInfo("Mock"), null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutCopyFileToDestination()
        {
            Assert.Throws<ArgumentNullException>(() => new ExternalScript(new MockFileSystemInfo("Mock"), new PowershellTranslator(), null));
        }
    }
}
