namespace ScoopBox.CommandBuilders
{
    public interface ICommandBuilder
    {
        string Build(string fullScriptName);

        string Build(string fullScriptName, string[] argumentsBeforeScript, string[] argumentsAfterScript);
    }
}
