using ScoopBox.Scripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts
{
    public class ScriptBuilder : IScriptBuilder
    {
        private readonly IExecutionPolicyBuilder setExecutionPolicy;
        private readonly IScoopInstallerBuilder scoopInstaller;
        private readonly IScoopBucketsBuilder scoopBuckets;
        private readonly IAppInstallerBuilder appInstaller;

        public ScriptBuilder(
            IExecutionPolicyBuilder setExecutionPolicy,
            IScoopInstallerBuilder scoopInstaller,
            IScoopBucketsBuilder scoopBuckets,
            IAppInstallerBuilder appInstaller)
        {
            this.setExecutionPolicy = setExecutionPolicy;
            this.scoopInstaller = scoopInstaller;
            this.scoopBuckets = scoopBuckets;
            this.appInstaller = appInstaller;
        }

        public string Build(ScoopBoxOptions scoopBoxOptions)
        {
            StringBuilder installerBuilder = new StringBuilder();

            installerBuilder.AppendLine(this.setExecutionPolicy.Build());
            installerBuilder.AppendLine(this.scoopInstaller.Build());
            installerBuilder.AppendLine(this.scoopBuckets.Build("extras"));
            installerBuilder.AppendLine(this.appInstaller.Build(scoopBoxOptions.Apps));

            return installerBuilder.ToString();
        }
    }
}
