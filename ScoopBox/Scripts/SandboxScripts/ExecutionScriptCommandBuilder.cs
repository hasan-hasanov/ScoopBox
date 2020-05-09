using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts.SandboxScripts
{
    public class ExecutionScriptCommandBuilder : IExecutionScriptCommandBuilder
    {
        public string Build(ScoopBoxOptions options)
        {
            StringBuilder executionPolicy = new StringBuilder();

            executionPolicy.Append("powershell.exe ");
            executionPolicy.Append($"\"{options.SandboxFilesPath}\\{Constants.InstallerName}\"");

            executionPolicy.AppendLine();
            return executionPolicy.ToString();
        }
    }
}
