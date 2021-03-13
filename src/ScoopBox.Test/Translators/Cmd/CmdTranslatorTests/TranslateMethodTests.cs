using ScoopBox.Test.Mocks;
using System;
using Xunit;

namespace ScoopBox.Test.Translators.Cmd.CmdTranslatorTests
{
    public class TranslateMethodTests
    {
        [Fact]
        public void ShouldThrowExceptionWithoutFile()
        {
            CmdTranslator translator = new CmdTranslator();
            Assert.Throws<ArgumentNullException>(() => translator.Translate(null, new Options()));
        }

        [Fact]
        public void ShouldThrowExceptionWithoutOptions()
        {
            string fileName = @"C:\mockFile.cmd";
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            CmdTranslator translator = new CmdTranslator();
            Assert.Throws<ArgumentNullException>(() => translator.Translate(mockFileInfo, null));
        }

        [Fact]
        public void ShouldTranslateBatScriptToCorrectPowershellCommandWithoutArguments()
        {
            string fileName = @"C:\mockFile.cmd";
            IOptions options = new Options();
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            CmdTranslator translator = new CmdTranslator(null, () => 1000);
            string result = translator.Translate(mockFileInfo, options);

            string actual = result;
            string expected = @"powershell.exe ""C:\Users\WDAGUtilityAccount\Desktop\Sandbox\mockFile.cmd"" 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log_1000.txt""";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldTranslatePowershellScriptToCorrectPowershellCommandWithArguments()
        {
            string fileName = @"C:\mockFile.cmd";
            IOptions options = new Options();
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            CmdTranslator translator = new CmdTranslator(new string[] { "test" }, () => 1000);
            string result = translator.Translate(mockFileInfo, options);

            string actual = result;
            string expected = @"powershell.exe ""C:\Users\WDAGUtilityAccount\Desktop\Sandbox\mockFile.cmd"" 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log_1000.txt"" test";

            Assert.Equal(expected, actual);
        }
    }
}
