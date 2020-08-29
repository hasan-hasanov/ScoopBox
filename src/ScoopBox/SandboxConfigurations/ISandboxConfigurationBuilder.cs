using System.Collections.Generic;

namespace ScoopBox.SandboxConfigurations
{
    public interface ISandboxConfigurationBuilder
    {
        IList<string> Commands { get; }

        void AddCommand(string command);

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
