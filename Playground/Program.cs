using Microsoft.Extensions.DependencyInjection;
using ScoopBox.Abstract;
using ScoopBox.Extensions;

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
            string scoop = scoopBuilder.Build();
        }
    }
}
