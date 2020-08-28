using ScoopBox.Enums;
using System.Collections.Generic;

namespace ScoopBox.SandboxConfigurations
{
    public class SandboxConfigurationOptions
    {
        public VGpuOptions VGpu { get; set; } = VGpuOptions.Disabled;

        public NetworkingOptions Networking { get; set; } = NetworkingOptions.Default;

        public AudioInputOptions AudioInput { get; set; } = AudioInputOptions.Default;

        public VideoInputOptions VideoInput { get; set; } = VideoInputOptions.Default;

        public ProtectedClientOptions ProtectedClient { get; set; } = ProtectedClientOptions.Default;

        public PrinterRedirectionOptions PrinterRedirection { get; set; } = PrinterRedirectionOptions.Default;

        public ClipboardRedirectionOptions ClipboardRedirection { get; set; } = ClipboardRedirectionOptions.Default;

        public int MemoryInMB { get; set; } = 0;

        public Dictionary<string, ReadOnlyOptions> UserMappedDirectories { get; set; } = new Dictionary<string, ReadOnlyOptions>();
    }
}
