using Microsoft.Extensions.DependencyInjection;
using ScoopBox.Abstract;
using ScoopBox.Scoop;
using ScoopBox.Scripts;
using ScoopBox.Scripts.Abstract;

namespace ScoopBox.Extensions
{
    public static class Abstractions
    {
        public static IServiceCollection UseScoopBox(this IServiceCollection services)
        {
            services.AddSingleton<IScoopBuilder, ScoopBuilder>();

            services.AddSingleton<ISetExecutionPolicy, SetExecutionPolicy>();
            services.AddSingleton<IScoopInstaller, ScoopInstaller>();
            services.AddSingleton<IScoopBuckets, ScoopBuckets>();
            services.AddSingleton<IAppInstaller, AppInstaller>();

            return services;
        }
    }
}
