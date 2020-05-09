using Microsoft.Extensions.DependencyInjection;
using ScoopBox.Scoop;
using ScoopBox.Scoop.Abstract;
using ScoopBox.Scripts;
using ScoopBox.Scripts.Abstract;

namespace ScoopBox.Extensions
{
    public static class Abstractions
    {
        public static IServiceCollection UseScoopBox(this IServiceCollection services)
        {
            services.AddSingleton<IScoopScriptGenerator, ScoopScriptGenerator>();

            services.AddSingleton<IExecutionPolicyBuilder, ExecutionPolicyBuilder>();
            services.AddSingleton<IScoopInstallerBuilder, ScoopInstallerBuilder>();
            services.AddSingleton<IScoopBucketsBuilder, ScoopBucketsBuilder>();
            services.AddSingleton<IAppInstallerBuilder, AppInstallerBuilder>();
            services.AddSingleton<IScriptBuilder, ScriptBuilder>();

            return services;
        }
    }
}
