using ScoopBox.Enums;
using System.Collections.Generic;
using System.IO;

namespace ScoopBox
{
    public class ScoopBoxOptions
    {
        public ScoopBoxOptions(List<string> apps)
            : this(apps, VGpuOptions.Disabled)
        {
        }

        public ScoopBoxOptions(
            List<string> apps,
            VGpuOptions gpuOptions)
        {
            Apps = apps;
            this.VGpu = gpuOptions;

        }

        public List<string> Apps { get; set; }

        public string SandboxFilesPath => Directory.CreateDirectory($"{Path.GetTempPath()}/{Constants.SandboxFolder}").FullName;

        public VGpuOptions VGpu { get; set; }
    }
}
