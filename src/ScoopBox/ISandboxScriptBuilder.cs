using ScoopBox.Entities;

namespace ScoopBox
{
    public interface ISandboxScriptBuilder
    {
        void BuildVGpu();
        void BuildNetworking();
        void BuildMappedFolders();
        void BuildLogonCommand();
        string Build();
    }
}
