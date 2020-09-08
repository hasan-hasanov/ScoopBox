using System.Collections.Generic;
using System.Threading.Tasks;

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

        Task CreatePartialConfigurationFile();

        Task CreateConfigurationFile();
    }
}
