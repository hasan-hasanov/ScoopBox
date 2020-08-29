using System;
using System.Collections.Generic;
using System.Text;

namespace ScoopBox.CommandBuilders
{
    public class CmdCommandBuilder : ICommandBuilder
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

            StringBuilder sbCmdCommandBuilder = new StringBuilder();
            sbCmdCommandBuilder
                .Append("cmd.exe")
                .Append(" ");

            if (argumentsBeforeScript?.Length > 0)
            {
                string beforeArguments = string.Join(" ", argumentsBeforeScript);

                sbCmdCommandBuilder
                    .Append(beforeArguments)
                    .Append(" ");
            }

            sbCmdCommandBuilder
                .Append($"{fullScriptName}");

            if (argumentsAfterScript?.Length > 0)
            {
                string afterArguments = string.Join(" ", argumentsAfterScript);

                sbCmdCommandBuilder
                    .Append(afterArguments);
            }

            return new List<string>() { sbCmdCommandBuilder.ToString() };
        }
    }
}
