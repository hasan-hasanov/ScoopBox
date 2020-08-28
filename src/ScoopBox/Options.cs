using ScoopBox.Enums;
using System.Collections.Generic;
using System.IO;

namespace ScoopBox
{
    public class Options
    {
        public Dictionary<string, ReadOnlyOptions> UserMappedDirectories { get; set; } = new Dictionary<string, ReadOnlyOptions>();

        public VGpuOptions VGpu { get; set; } = VGpuOptions.Disabled;

        public NetworkingOptions Networking { get; set; } = NetworkingOptions.Default;
    }
}
