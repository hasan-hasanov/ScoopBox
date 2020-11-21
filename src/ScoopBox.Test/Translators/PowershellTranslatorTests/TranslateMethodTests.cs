using ScoopBox.Test.Mocks;
using ScoopBox.Translators.Powershell;
using System;
using Xunit;

namespace ScoopBox.Test.Translators.PowershellTranslatorTests
{
    public class TranslateMethodTests
    {
        [Fact]
        public void ShouldThrowExceptionWithoutFile()
        {
            PowershellTranslator translator = new PowershellTranslator();
            Assert.Throws<ArgumentNullException>(() => translator.Translate(null, new Options()));
        }

        [Fact]
        public void ShouldThrowExceptionWithoutOptions()
        {
            string fileName = @"C:\mockFile.ps1";
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            PowershellTranslator translator = new PowershellTranslator();
            Assert.Throws<ArgumentNullException>(() => translator.Translate(mockFileInfo, null));
        }

        [Fact]
        public void ShouldTranslatePowershellScriptToCorrectPowershellCommandWithoutArguments()
        {
            string fileName = @"C:\mockFile.ps1";
            IOptions options = new Options();
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            PowershellTranslator powershellTranslator = new PowershellTranslator();
            string result = powershellTranslator.Translate(mockFileInfo, options);

            string actual = result;
            string expected = @"powershell.exe -ExecutionPolicy Bypass -File C:\Users\WDAGUtilityAccount\Desktop\Sandbox\mockFile.ps1 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log.txt""";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldTranslatePowershellScriptToCorrectPowershellCommandWithArguments()
        {
            string fileName = @"C:\mockFile.ps1";
            IOptions options = new Options();
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            PowershellTranslator powershellTranslator = new PowershellTranslator(new string[] { "test" });
            string result = powershellTranslator.Translate(mockFileInfo, options);

            string actual = result;
            string expected = @"powershell.exe -ExecutionPolicy Bypass -File C:\Users\WDAGUtilityAccount\Desktop\Sandbox\mockFile.ps1 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log.txt"" test";

            Assert.Equal(expected, actual);
        }
    }
}
