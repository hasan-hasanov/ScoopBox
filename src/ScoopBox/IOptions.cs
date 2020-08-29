using ScoopBox.Entities;
using ScoopBox.Enums;
using System.Collections.Generic;

namespace ScoopBox
{
    public interface IOptions
    {
        string SandboxBeforeScriptsLocation { get; }

        string SandboxAfterScriptsLocation { get; }

        string SandboxPackageManagerScriptsLocation { get; }

        string SandboxConfigurationFileName { get; }

        string RootFilesDirectoryLocation { get; }

        string RootSandboxFilesDirectoryLocation { get; }

        VGpuOptions VGpu { get; }

        NetworkingOptions Networking { get; }

        AudioInputOptions AudioInput { get; }

        VideoInputOptions VideoInput { get; }

        ProtectedClientOptions ProtectedClient { get; }

        PrinterRedirectionOptions PrinterRedirection { get; }

        ClipboardRedirectionOptions ClipboardRedirection { get; }

        int MemoryInMB { get; }

        IEnumerable<MappedFolder> UserMappedDirectories { get; }
    }
}
