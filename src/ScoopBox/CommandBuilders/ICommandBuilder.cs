using System.Collections.Generic;

namespace ScoopBox.CommandBuilders
{
    public interface ICommandBuilder
    {
        IEnumerable<string> Build(string fullScriptName);

        IEnumerable<string> Build(string fullScriptName, string[] argumentsBeforeScript, string[] argumentsAfterScript);
    }
}
