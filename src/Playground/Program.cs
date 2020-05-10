using Microsoft.Extensions.DependencyInjection;
using ScoopBox;
using ScoopBox.Extensions;
using ScoopBox.Sandbox.Abstract;
using ScoopBox.SandboxProcess.Abstract;
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

            var scoopScriptGenerator = serviceProvider.GetService<IScoopScript>();
            var sandboxScriptGenerator = serviceProvider.GetService<ISandboxScript>();
            var scoopBoxProcess = serviceProvider.GetService<IScoopBoxProcess>();

            var apps = new List<string>() { "git", "curl", "openssh", "vscode", "fiddler" };
            ScoopBoxOptions options = new ScoopBoxOptions(apps);

            await scoopScriptGenerator.Generate(options);
            await sandboxScriptGenerator.Generate(options);
            await scoopBoxProcess.Run(options);
        }
    }
}
