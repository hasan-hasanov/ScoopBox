using ScoopBox.Abstract;
using ScoopBox.Scripts.Abstract;
using System.Text;

namespace ScoopBox.Scoop
{
    public class ScoopBuilder : IScoopBuilder
    {
        private readonly ISetExecutionPolicy setExecutionPolicy;
        private readonly IScoopInstaller scoopInstaller;

        public ScoopBuilder(
            ISetExecutionPolicy setExecutionPolicy,
            IScoopInstaller scoopInstaller)
        {
            this.setExecutionPolicy = setExecutionPolicy;
            this.scoopInstaller = scoopInstaller;
        }

        public string Build()
        {
            StringBuilder installerBuilder = new StringBuilder();

            string executionPolicy = this.setExecutionPolicy.Set();
            installerBuilder.AppendLine(executionPolicy);

            string scoopInstaller = this.scoopInstaller.Install();
            installerBuilder.AppendLine(scoopInstaller);

            return installerBuilder.ToString();
        }
    }
}
