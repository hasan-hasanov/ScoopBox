using ScoopBox.Entities;

namespace ScoopBox
{
    public interface ISandboxScriptBuilder
    {
        void BuildExecutionPolicy();
        void BuildExecutionScript();
        void BuildMappedFolders();
        Configuration Build();
    }
}
