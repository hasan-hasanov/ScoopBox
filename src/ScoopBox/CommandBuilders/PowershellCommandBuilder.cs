using System;
using System.Text;

namespace ScoopBox.CommandBuilders
{
    public class PowershellCommandBuilder : ICommandBuilder
    {
        public string Build(string fullScriptName)
        {
            return Build(fullScriptName, null, null);
        }

        public string Build(string fullScriptName, string[] argumentsBeforeScript, string[] argumentsAfterScript)
        {
            if (string.IsNullOrWhiteSpace(fullScriptName))
            {
                throw new ArgumentNullException(nameof(fullScriptName));
            }

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
                    .Append(afterArguments);
            }

            return sbPowershellCommandBuilder.ToString();
        }
    }
}
