using Microsoft.Extensions.DependencyInjection;
using ScoopBox;
using ScoopBox.Extensions;
using ScoopBox.Sandbox.Abstract;
using ScoopBox.Scoop.Abstract;
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

            var scoopScriptGenerator = serviceProvider.GetService<IScoopScriptGenerator>();
            var sandboxScriptGenerator = serviceProvider.GetService<ISandboxScriptGenerator>();

            var apps = new List<string>() { "git", "curl", "openssh" };
            ScoopBoxOptions options = new ScoopBoxOptions(apps);

            await scoopScriptGenerator.Generate(options);
            await sandboxScriptGenerator.Generate(options);
            ScoopBoxProcess process = new ScoopBoxProcess();
            process.Run(options);
        }
    }
}
