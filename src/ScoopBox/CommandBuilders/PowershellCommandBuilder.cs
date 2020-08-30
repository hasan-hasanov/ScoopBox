using System;
using System.Collections.Generic;
using System.Text;

namespace ScoopBox.CommandBuilders
{
    public class PowershellCommandBuilder : ICommandBuilder
    {
        public IEnumerable<string> Build(string fullScriptName)
        {
            return Build(fullScriptName, null, null);
        }

        public IEnumerable<string> Build(string fullScriptName, string[] argumentsBeforeScript, string[] argumentsAfterScript)
        {
            if (string.IsNullOrWhiteSpace(fullScriptName))
            {
                throw new ArgumentNullException(nameof(fullScriptName));
            }

            string executionPolicy = BuildPowershellExecutionPolicy(fullScriptName);
            string executionCommand = BuildPowershellExecutionCommand(fullScriptName, argumentsBeforeScript, argumentsAfterScript);

            return new List<string>() { executionPolicy, executionCommand };
        }

        private string BuildPowershellExecutionPolicy(string fullScriptLocation)
        {
            StringBuilder sbExecutionPolicy = new StringBuilder();

            sbExecutionPolicy
                .Append("powershell.exe -ExecutionPolicy Bypass -File")
                .Append(" ")
                .Append(fullScriptLocation);

            return sbExecutionPolicy.ToString();
        }

        private string BuildPowershellExecutionCommand(string fullScriptName, string[] argumentsBeforeScript, string[] argumentsAfterScript)
        {
            StringBuilder sbPowershellCommandBuilder = new StringBuilder();
            sbPowershellCommandBuilder
                .Append("powershell.exe")
                .Append(" ");

            if (argumentsBeforeScript?.Length > 0)
            {
                string beforeArguments = string.Join(" ", argumentsBeforeScript);

                sbPowershellCommandBuilder
                    .Append(beforeArguments)
                    .Append(" ");
            }

            sbPowershellCommandBuilder
                .Append($"{fullScriptName}");

            if (argumentsAfterScript?.Length > 0)
            {
                string afterArguments = string.Join(" ", argumentsAfterScript);

                sbPowershellCommandBuilder
                    .Append(" ")
                    .Append(afterArguments);
            }

            return sbPowershellCommandBuilder.ToString();
        }
    }
}
