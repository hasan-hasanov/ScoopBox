using ScoopBox.Scripts.Materialized;
using ScoopBox.Test.Mocks;
using ScoopBox.Translators;
using ScoopBox.Translators.Powershell;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.Scripts.Materialized.ExternalScriptTests
{
    public class ProcessMethodTests
    {
        [Fact]
        public async Task ShouldCopyFromCorrectSource()
        {
            string fileName = @"C:\MockFileName.ps1";
            IOptions options = new Options();
            MockFileSystemInfo fileInfo = new MockFileSystemInfo(fileName);
            IPowershellTranslator translator = new PowershellTranslator();

            string sourcePath = string.Empty;

            ExternalScript externalScript = new ExternalScript(fileInfo, translator, (source, destination, token) =>
            {
                sourcePath = source;
                return Task.CompletedTask;
            });

            await externalScript.Process(options);

            string expected = fileName;
            string actual = sourcePath;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldCopyToCorrectDestination()
        {
            string fileName = @"C:\MockFileName.ps1";
            IOptions options = new Options();
            MockFileSystemInfo fileInfo = new MockFileSystemInfo(fileName);
            IPowershellTranslator translator = new PowershellTranslator();

            string destinationPath = string.Empty;

            ExternalScript externalScript = new ExternalScript(fileInfo, translator, (source, destination, token) =>
            {
                destinationPath = destination;

                return Task.CompletedTask;
            });

            await externalScript.Process(options);

            string expected = Path.Combine(options.RootFilesDirectoryLocation, Path.GetFileName(externalScript.ScriptFile.Name));
            string actual = destinationPath;

            Assert.Equal(expected, actual);
        }
    }
}
