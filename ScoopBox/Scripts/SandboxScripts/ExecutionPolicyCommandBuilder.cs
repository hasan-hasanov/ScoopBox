using ScoopBox.Scripts.InstallerScripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts.InstallerScripts
{
    public class ExecutionPolicyCommandBuilder : IExecutionPolicyCommandBuilder
    {
        public string Build(ScoopBoxOptions options)
        {
            StringBuilder executionPolicy = new StringBuilder();

            executionPolicy.Append("powershell.exe ");
            executionPolicy.Append("-ExecutionPolicy ");
            executionPolicy.Append("Bypass ");
            executionPolicy.Append("-File ");
            executionPolicy.Append($@"{options.SandboxFilesPath}\sandbox.ps1");

            executionPolicy.AppendLine();
            return executionPolicy.ToString();
        }
    }
}
