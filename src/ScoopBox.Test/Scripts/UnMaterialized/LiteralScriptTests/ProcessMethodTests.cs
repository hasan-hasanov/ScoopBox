using ScoopBox.Scripts.UnMaterialized;
using ScoopBox.Translators;
using ScoopBox.Translators.Powershell;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.Scripts.UnMaterialized.LiteralScriptTests
{
    public class ProcessMethodTests
    {
        [Fact]
        public async Task ShouldCopyFromCorrectSource()
        {
            string fileName = @"C:\MockFileName.ps1";
            IList<string> commands = new List<string>()
            {
                @"Start-Process 'C:\windows\system32\notepad.exe'",
                @"Start-Process .",
            };

            IOptions options = new Options();
            IPowershellTranslator translator = new PowershellTranslator();
            string sourceFilePath = string.Empty;

            LiteralScript literalScript = new LiteralScript(
                commands,
                translator,
                fileName,
                (path, content, token) =>
                {
                    sourceFilePath = path;
                    return Task.CompletedTask;
                });
            await literalScript.Process(options);

            string expected = fileName;
            string actual = sourceFilePath;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldGenerateCorrectContent()
        {
            string fileName = @"C:\MockFileName.ps1";
            IList<string> commands = new List<string>()
            {
                @"Start-Process 'C:\windows\system32\notepad.exe'",
                @"Start-Process .",
            };

            IOptions options = new Options();
            IPowershellTranslator translator = new PowershellTranslator();
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            string strContent = string.Empty;

            LiteralScript literalScript = new LiteralScript(
                commands,
                translator,
                fileName,
                (path, content, token) =>
                {
                    strContent = uTF8Encoding.GetString(content);
                    return Task.CompletedTask;
                });
            await literalScript.Process(options);

            string expected = @"Start-Process 'C:\windows\system32\notepad.exe'
Start-Process .";
            string actual = strContent;

            Assert.Equal(expected, actual);
        }
    }
}
