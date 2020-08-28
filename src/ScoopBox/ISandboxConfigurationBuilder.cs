using ScoopBox.Entities;

namespace ScoopBox
{
    public interface ISandboxConfigurationBuilder
    {
        void BuildVGpu();
        void BuildNetworking();
        void BuildMappedFolders();
        void BuildLogonCommand();
        string Build();
    }
}
