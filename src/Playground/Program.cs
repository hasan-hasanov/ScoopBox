using ScoopBox;
using ScoopBox.CommandBuilders;
using ScoopBox.PackageManager;
using ScoopBox.PackageManager.Scoop;
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
            await sandbox2.Run(new List<Tuple<FileSystemInfo, ICommandBuilder>>()
            {
                Tuple.Create<FileSystemInfo, ICommandBuilder>(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\test2.ps1"), new PowershellCommandBuilder()),
                Tuple.Create<FileSystemInfo, ICommandBuilder>(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\test.ps1"), new PowershellCommandBuilder())
            });



            //ISandbox sandbox3 = new Sandbox();
            //await sandbox3.Run(@"New-Item 'C:\Users\WDAGUtilityAccount\Desktop\TestText.txt'", new PowershellCommandBuilder());
        }
    }
}
