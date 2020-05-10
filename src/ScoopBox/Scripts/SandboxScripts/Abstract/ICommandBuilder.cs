using System.Collections.Generic;

namespace ScoopBox.Scripts.SandboxScripts.Abstract
{
    public interface ICommandBuilder
    {
        List<string> Build(ScoopBoxOptions options);
    }
}
