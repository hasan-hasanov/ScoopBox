using Microsoft.Extensions.DependencyInjection;
using ScoopBox.Sandbox;
using ScoopBox.Sandbox.Abstract;
using ScoopBox.Scoop;
using ScoopBox.Scoop.Abstract;
using ScoopBox.Scripts.InstallerScripts;
using ScoopBox.Scripts.InstallerScripts.Abstract;
using ScoopBox.Scripts.SandboxScripts;
using ScoopBox.Scripts.SandboxScripts.Abstract;

namespace ScoopBox.Extensions
{
    public static class Abstractions
    {
        public static IServiceCollection UseScoopBox(this IServiceCollection services)
        {
            services.AddScoped<IScoopScriptGenerator, ScoopScriptGenerator>();
            services.AddScoped<ISandboxScriptGenerator, SandboxScriptGenerator>();

            services.AddScoped<IExecutionPolicyBuilder, ExecutionPolicyBuilder>();
            services.AddScoped<IScoopInstallerBuilder, ScoopInstallerBuilder>();
            services.AddScoped<IScoopBucketsBuilder, ScoopBucketsBuilder>();
            services.AddScoped<IAppInstallerBuilder, AppInstallerBuilder>();
            services.AddScoped<IInstallerScriptBuilder, InstallerScriptBuilder>();

            services.AddScoped<ISandboxScriptBuilder, SandboxScriptBuilder>();

            return services;
        }
    }
}
