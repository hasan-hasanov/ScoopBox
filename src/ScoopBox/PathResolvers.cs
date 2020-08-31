using System;
using System.IO;

namespace ScoopBox
{
    public static class PathResolvers
    {
        public static string GetBeforeScriptsPath(string rootFilesDirectoryLocation)
        {
            if (string.IsNullOrWhiteSpace(rootFilesDirectoryLocation))
            {
                throw new ArgumentNullException(nameof(rootFilesDirectoryLocation));
            }

            return Path.Combine(rootFilesDirectoryLocation, "BeforeScripts");
        }

        public static string GetAfterScriptsPath(string rootFilesDirectoryLocation)
        {
            if (string.IsNullOrWhiteSpace(rootFilesDirectoryLocation))
            {
                throw new ArgumentNullException(nameof(rootFilesDirectoryLocation));
            }

            return Path.Combine(rootFilesDirectoryLocation, "AfterScripts");
        }

        public static string GetPackageManagerScriptsPath(string rootFilesDirectoryLocation)
        {
            if (string.IsNullOrWhiteSpace(rootFilesDirectoryLocation))
            {
                throw new ArgumentNullException(nameof(rootFilesDirectoryLocation));
            }

            return Path.Combine(rootFilesDirectoryLocation, "PackageManagerScripts");
        }
    }
}
