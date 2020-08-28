using ScoopBox;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ISandbox sandbox = new Sandbox();
            await sandbox.Run();
        }
    }
}
