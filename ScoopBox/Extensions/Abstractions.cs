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
            services.AddSingleton<IScriptGenerator, ScriptGenerator>();

            services.AddSingleton<IExecutionPolicyBuilder, ExecutionPolicyBuilder>();
            services.AddSingleton<IScoopInstallerBuilder, ScoopInstallerBuilder>();
            services.AddSingleton<IScoopBucketsBuilder, ScoopBucketsBuilder>();
            services.AddSingleton<IAppInstallerBuilder, AppInstallerBuilder>();
            services.AddSingleton<IScriptBuilder, ScriptBuilder>();

            return services;
        }
    }
}
