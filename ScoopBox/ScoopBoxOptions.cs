using ScoopBox.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace ScoopBox
{
    public class ScoopBoxOptions
    {
        public ScoopBoxOptions(List<string> apps)
            : this(apps, Path.GetTempPath())
        {
        }

        public ScoopBoxOptions(List<string> apps, string sandboxFilesPath)
            : this(apps, sandboxFilesPath, VGpuOptions.Disabled)
        {
        }

        public ScoopBoxOptions(
            List<string> apps,
            string sandboxFilesPath,
            VGpuOptions gpuOptions)
        {
            Apps = apps;

            SandboxFilesPath = Path
                .GetFullPath(new Uri(sandboxFilesPath).LocalPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                .ToUpperInvariant();

            this.VGpu = gpuOptions;

        }

        public List<string> Apps { get; set; }

        public string SandboxFilesPath { get; set; }

        public VGpuOptions VGpu { get; set; }
    }
}
