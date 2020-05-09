using ScoopBox.Scripts.InstallerScripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts.InstallerScripts
{
    public class ExecutionPolicyBuilder : IExecutionPolicyBuilder
    {
        public string Build()
        {
            StringBuilder executionPolicy = new StringBuilder();

            executionPolicy.Append("Set-ExecutionPolicy ");
            executionPolicy.Append("Bypass ");
            executionPolicy.Append("-Force");

            executionPolicy.AppendLine();
            return executionPolicy.ToString();
        }
    }
}
