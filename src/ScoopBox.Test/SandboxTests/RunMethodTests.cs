using ScoopBox.SandboxConfigurations;
using ScoopBox.Scripts;
using ScoopBox.Scripts.Materialized;
using ScoopBox.Scripts.PackageManagers.Scoop;
using ScoopBox.Scripts.UnMaterialized;
using ScoopBox.Test.Mocks;
using ScoopBox.Translators;
using ScoopBox.Translators.Powershell;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ScoopBox.Test.SandboxTests
{
    public class RunMethodTests
    {
        [Fact]
        public async Task ShouldBuilCorrectMainScriptContent()
        {
            IList<string> actualScripts = new List<string>();

            IOptions options = new Options();
            ISandboxConfigurationBuilder configurationBuilder = new SandboxConfigurationBuilder(new Options());
            Action<string> createDirectory = path => { };
            Action<string> deleteFiles = path => { };
            Action<string> deleteDirectories = path => { };
            Func<string, Task> startProcess = path => Task.CompletedTask;
            Func<string, byte[], CancellationToken, Task> writeAllBytes = (path, content, token) => Task.CompletedTask;
            Func<string, string, CancellationToken, Task> copyFiles = (source, destination, token) => Task.CompletedTask;
            Func<IList<string>, IPowershellTranslator, string, IOptions, Task<LiteralScript>> literalScriptFactory = async (scripts, translator, name, options) =>
            {
                actualScripts = scripts;
                var literalScript = new LiteralScript(scripts, translator, name, deleteFiles, writeAllBytes);
                await literalScript.Process(options);
                return literalScript;
            };

            var sandbox = new Sandbox(options, configurationBuilder, createDirectory, deleteFiles, deleteDirectories, startProcess, literalScriptFactory);
            await sandbox.Run(new List<IScript>()
            {
                // Raw script that will be executed
                new LiteralScript(new List<string>() {
                    @"Start-Process 'C:\windows\system32\notepad.exe'" },
                    new PowershellTranslator(),
                    "mockScriptName1.ps1",
                    deleteFiles,
                    writeAllBytes),

                // Script that will open the browser
                new ExternalScript(
                    new MockFileSystemInfo(@"C:\Users\OpenBrowserScript.ps1"),
                    new PowershellTranslator(),
                    copyFiles),

                // Script that will open explorer
                new ExternalScript(
                    new MockFileSystemInfo(@"C:\Users\OpenExplorerScript.ps1"),
                    new PowershellTranslator(),
                    copyFiles),

                // Script that will install curl and fiddler using scoop package manager
                new ScoopPackageManagerScript(
                    new List<string>(){ "curl", "fiddler" },
                    new PowershellTranslator(),
                    "mockScriptName2",
                    new StringBuilder(),
                    writeAllBytes),
            });

            IList<string> expected = new List<string>()
            {
                @"powershell.exe -ExecutionPolicy Bypass -File C:\Users\WDAGUtilityAccount\Desktop\Sandbox\mockScriptName1.ps1 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log.txt""",
                @"powershell.exe -ExecutionPolicy Bypass -File C:\Users\WDAGUtilityAccount\Desktop\Sandbox\OpenBrowserScript.ps1 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log.txt""",
                @"powershell.exe -ExecutionPolicy Bypass -File C:\Users\WDAGUtilityAccount\Desktop\Sandbox\OpenExplorerScript.ps1 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log.txt""",
                @"powershell.exe -ExecutionPolicy Bypass -File C:\Users\WDAGUtilityAccount\Desktop\Sandbox\mockScriptName2 3>&1 2>&1 > ""C:\Users\WDAGUtilityAccount\Desktop\Log.txt""",
            };
            IList<string> actual = actualScripts;

            Assert.Equal(expected, actual);
        }
    }
}
