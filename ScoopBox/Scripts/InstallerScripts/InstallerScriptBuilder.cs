using ScoopBox.Scripts.InstallerScripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts.InstallerScripts
{
    public class InstallerScriptBuilder : IInstallerScriptBuilder
    {
        private readonly IExecutionPolicyBuilder setExecutionPolicy;
        private readonly IScoopInstallerBuilder scoopInstaller;
        private readonly IScoopBucketsBuilder scoopBuckets;
        private readonly IAppInstallerBuilder appInstaller;

        public InstallerScriptBuilder(
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

            installerBuilder.AppendLine(setExecutionPolicy.Build());
            installerBuilder.AppendLine(scoopInstaller.Build());
            installerBuilder.AppendLine(scoopBuckets.Build("extras"));
            installerBuilder.AppendLine(appInstaller.Build(scoopBoxOptions.Apps));

            return installerBuilder.ToString();
        }
    }
}
