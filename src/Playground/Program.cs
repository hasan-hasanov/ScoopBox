using System.IO;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FileStream test = File.OpenRead("");
            FileStream.Synchronized(File.OpenRead(""));
        }
    }
}
