using ScoopBox.Enums;
using System.Collections.Generic;
using System.IO;

namespace ScoopBox
{
    public class ScoopBoxOptions
    {
        public string UserFilesPath { get; set; } = Directory.CreateDirectory($"{Path.GetTempPath()}/{Constants.SandboxFolderName}").FullName;

        public VGpuOptions VGpu { get; set; } = VGpuOptions.Disabled;

        public NetworkingOptions Networking { get; set; } = NetworkingOptions.Default;
    }
}
