using ScoopBox;
using ScoopBox.CommandBuilders;
using ScoopBox.PackageManager;
using ScoopBox.PackageManager.Scoop;
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

            //ISandbox sandbox2 = new Sandbox();
            //await sandbox2.Run(File.OpenRead(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\test.ps1"), new PowershellCommandBuilder());

            ISandbox sandbox3 = new Sandbox();
            await sandbox3.Run(@"Start-Process 'C:\windows\system32\notepad.exe'", new PowershellCommandBuilder());
        }
    }
}
