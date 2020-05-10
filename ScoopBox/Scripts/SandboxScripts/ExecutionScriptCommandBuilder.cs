using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts.SandboxScripts
{
    public class ExecutionScriptCommandBuilder : IExecutionScriptCommandBuilder
    {
        public string Build()
        {
            StringBuilder executionPolicy = new StringBuilder();

            executionPolicy.Append("powershell.exe ");
            executionPolicy.Append($"\"{Constants.SandboxInstallerLocation}\"");

            executionPolicy.AppendLine();
            return executionPolicy.ToString();
        }
    }
}
