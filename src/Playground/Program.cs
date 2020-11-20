using ScoopBox;
using ScoopBox.Scripts;
using ScoopBox.Scripts.Materialized;
using ScoopBox.Scripts.PackageManagers.Scoop;
using ScoopBox.Scripts.UnMaterialized;
using ScoopBox.Translators;
using ScoopBox.Translators.Powershell;
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
            ISandbox sandbox = new Sandbox();
            await sandbox.Run(new List<Tuple<IScript, IPowershellTranslator>>()
            {
                Tuple.Create<IScript, IPowershellTranslator>(new LiteralScript(new List<string>() { @"Start-Process 'C:\windows\system32\notepad.exe'" }), new PowershellTranslator()),
                Tuple.Create<IScript, IPowershellTranslator>(new ExternalScript(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\Scripts\facebook.ps1")), new PowershellTranslator()),
                Tuple.Create<IScript, IPowershellTranslator>(new ExternalScript(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\Scripts\explorer.ps1")), new PowershellTranslator()),
                Tuple.Create<IScript, IPowershellTranslator>(new ScoopPackageManager(new List<string>(){ "curl" }), new PowershellTranslator()),
            });
        }
    }
}
