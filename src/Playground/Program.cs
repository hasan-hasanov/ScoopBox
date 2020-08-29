using ScoopBox;
using ScoopBox.CommandBuilders;
using System.IO;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ISandbox sandbox = new Sandbox();
            await sandbox.Run(File.OpenRead(@"C:\Users\Hasan Hasanov\AppData\Local\Temp\test.txt"), new PowershellCommandBuilder());
        }
    }
}
