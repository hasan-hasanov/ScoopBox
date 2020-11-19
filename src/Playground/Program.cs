using ScoopBox;
using ScoopBox.Scripts;
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
            IScript script = new Script(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\Scripts\notepad.ps1"));
            IPowershellTranslator translator = new PowershellTranslator();

            ISandbox sandbox = new Sandbox();
            await sandbox.Run(new List<Tuple<IScript, IPowershellTranslator>>()
            {
                Tuple.Create(script, translator)
            });
        }
    }
}
