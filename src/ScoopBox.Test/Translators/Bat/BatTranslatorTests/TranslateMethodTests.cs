using ScoopBox.Test.Mocks;
using ScoopBox.Translators.Bat;
using System;
using Xunit;

namespace ScoopBox.Test.Translators.Bat.BatTranslatorTests
{
    public class TranslateMethodTests
    {
        [Fact]
        public void ShouldThrowExceptionWithoutFile()
        {
            BatTranslator translator = new BatTranslator();
            Assert.Throws<ArgumentNullException>(() => translator.Translate(null, new Options()));
        }

        [Fact]
        public void ShouldThrowExceptionWithoutOptions()
        {
            string fileName = @"C:\mockFile.bat";
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            BatTranslator translator = new BatTranslator();
            Assert.Throws<ArgumentNullException>(() => translator.Translate(mockFileInfo, null));
        }

        [Fact]
        public void ShouldTranslateBatScriptToCorrectPowershellCommandWithoutArguments()
        {
            string fileName = @"C:\mockFile.bat";
            IOptions options = new Options();
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            BatTranslator translator = new BatTranslator(null, () => 1000);
            string result = translator.Translate(mockFileInfo, options);

            string actual = result;
            string expected = @"powershell.exe ""C:\Users\WDAGUtilityAccount\Desktop\Sandbox\mockFile.bat"" 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log_1000.txt""";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldTranslatePowershellScriptToCorrectPowershellCommandWithArguments()
        {
            string fileName = @"C:\mockFile.bat";
            IOptions options = new Options();
            MockFileSystemInfo mockFileInfo = new MockFileSystemInfo(fileName);

            BatTranslator translator = new BatTranslator(new string[] { "test" }, () => 1000);
            string result = translator.Translate(mockFileInfo, options);

            string actual = result;
            string expected = @"powershell.exe ""C:\Users\WDAGUtilityAccount\Desktop\Sandbox\mockFile.bat"" 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log_1000.txt"" test";

            Assert.Equal(expected, actual);
        }
    }
}
