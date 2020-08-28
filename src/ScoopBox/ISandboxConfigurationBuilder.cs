using ScoopBox.Entities;

namespace ScoopBox
{
    public interface ISandboxConfigurationBuilder
    {
        void BuildVGpu();

        void BuildNetworking();

        void BuildAudioInput();

        void BuildVideoInput();

        void BuildProtectedClient();

        void BuildPrinterRedirection();

        void BuildClipboardRedirection();

        void BuildMemoryInMB();

        void BuildMappedFolders();

        void BuildLogonCommand();

        string BuildPartial();

        string Build();
    }
}
