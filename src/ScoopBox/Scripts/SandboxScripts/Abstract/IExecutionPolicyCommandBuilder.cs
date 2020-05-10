namespace ScoopBox.Scripts.InstallerScripts.Abstract
{
    public interface IExecutionPolicyCommandBuilder
    {
        string Build(ScoopBoxOptions options);
    }
}
