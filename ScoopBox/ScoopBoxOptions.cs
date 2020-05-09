using System;
using System.Collections.Generic;
using System.IO;

namespace ScoopBox
{
    public class ScoopBoxOptions
    {
        public ScoopBoxOptions(List<string> apps) : this(apps, Path.GetTempPath())
        {
        }

        public ScoopBoxOptions(List<string> apps, string sandboxFilesPath)
        {
            Apps = apps;

            SandboxFilesPath = Path
                .GetFullPath(new Uri(sandboxFilesPath).LocalPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                .ToUpperInvariant();
        }

        public List<string> Apps { get; set; }

        public string SandboxFilesPath { get; set; }
    }
}
