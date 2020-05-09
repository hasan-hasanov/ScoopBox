using ScoopBox.Entities;

namespace ScoopBox.Scripts.SandboxScripts.Abstract
{
    public interface ISandboxScriptBuilder
    {
        string Build(ScoopBoxOptions options);
    }
}
