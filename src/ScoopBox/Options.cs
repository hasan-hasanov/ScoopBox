using ScoopBox.Entities;
using ScoopBox.Enums;
using System.Collections.Generic;
using System.IO;

namespace ScoopBox
{
    public class Options : IOptions
    {
        public string SandboxBeforeScriptsLocation { get; set; } = "BeforeScripts";

        public string SandboxAfterScriptsLocation { get; set; } = "AfterScripts";

        public string SandboxPackageManagerScriptsLocation { get; set; } = "PackageManagerScripts";

        public string SandboxConfigurationFileName { get; set; } = "sandbox.wsb";

        public string RootSandboxFilesDirectoryLocation { get; set; } = @"C:\Users\WDAGUtilityAccount\Desktop\Sandbox\";

        public string RootFilesDirectoryLocation { get; set; } = Directory.CreateDirectory($"{Path.GetTempPath()}/Sandbox").FullName;

        public VGpuOptions VGpu { get; set; } = VGpuOptions.Disabled;

        public NetworkingOptions Networking { get; set; } = NetworkingOptions.Default;

        public AudioInputOptions AudioInput { get; set; } = AudioInputOptions.Default;

        public VideoInputOptions VideoInput { get; set; } = VideoInputOptions.Default;

        public ProtectedClientOptions ProtectedClient { get; set; } = ProtectedClientOptions.Default;

        public PrinterRedirectionOptions PrinterRedirection { get; set; } = PrinterRedirectionOptions.Default;

        public ClipboardRedirectionOptions ClipboardRedirection { get; set; } = ClipboardRedirectionOptions.Default;

        public int MemoryInMB { get; set; } = 0;

        public IEnumerable<MappedFolder> UserMappedDirectories { get; set; } = new List<MappedFolder>();
    }
}
