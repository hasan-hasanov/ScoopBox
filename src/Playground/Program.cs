using ScoopBox;
using ScoopBox.CommandTranslators;
using ScoopBox.CommandTranslators.Powershell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //ISandbox sandbox = new Sandbox();
            //await sandbox.Run(new Dictionary<IPackageManager, ICommandBuilder>()
            //{
            //    { new ScoopPackageManager(new List<string>() { "git", "curl" }), new PowershellCommandBuilder() }
            //});

            ISandbox sandbox2 = new Sandbox();
            await sandbox2.Run(new List<Tuple<FileSystemInfo, ICommandTranslator>>()
            {
                Tuple.Create<FileSystemInfo, ICommandTranslator>(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\test2.ps1"), new PowershellTranslator()),
                Tuple.Create<FileSystemInfo, ICommandTranslator>(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\test.ps1"), new PowershellTranslator())
            });

            //ISandbox sandbox3 = new Sandbox();
            //await sandbox3.Run(new List<string>()
            //{
            //    @"New-Item 'C:\Users\WDAGUtilityAccount\Desktop\TestText.txt'",
            //    @"New-Item 'C:\Users\WDAGUtilityAccount\Desktop\TestText2.txt'"
            //});
        }
    }
}
