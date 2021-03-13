using ScoopBox;
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
            await sandbox.Run(new List<IScript>()
            {
                new ExternalScript(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\Scripts\notepad.cmd"), new CmdTranslator()),
                new ExternalScript(new FileInfo(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\Scripts\notepads.bat"), new BatTranslator()),
            });
        }
    }
}
