using Microsoft.Extensions.DependencyInjection;
using ScoopBox;
using ScoopBox.Abstract;
using ScoopBox.Extensions;
using System.Collections.Generic;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
            .UseScoopBox()
            .BuildServiceProvider();

            var scoopBuilder = serviceProvider.GetService<IScoopBuilder>();

            ScoopBoxOptions options = new ScoopBoxOptions()
            {
                Apps = new List<string>()
                {
                    "curl",
                    "openssh"
                }
            };

            string scoop = scoopBuilder.BuildInstaller(options);
        }
    }
}
