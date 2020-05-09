using Microsoft.Extensions.DependencyInjection;
using ScoopBox;
using ScoopBox.Abstract;
using ScoopBox.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
            .UseScoopBox()
            .BuildServiceProvider();

            var scoopBuilder = serviceProvider.GetService<IScriptGenerator>();
            var apps = new List<string>() { "curl", "openssh" };
            string path = @"C:\Users\Hasan\Desktop\\";

            ScoopBoxOptions options = new ScoopBoxOptions(apps, path);

            await scoopBuilder.Generate(options);
        }
    }
}
