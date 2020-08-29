using System.IO;

namespace ScoopBox
{
    public static class PathResolvers
    {
        public static string GetBeforeScriptsPath(string rootFilesDirectoryLocation)
        {
            return Path.Combine(rootFilesDirectoryLocation, "BeforeScripts");
        }

        public static string GetAfterScriptsPath(string rootFilesDirectoryLocation)
        {
            return Path.Combine(rootFilesDirectoryLocation, "AfterScripts");
        }

        public static string GetPackageManagerScriptsPath(string rootFilesDirectoryLocation)
        {
            return Path.Combine(rootFilesDirectoryLocation, "PackageManagerScripts");
        }
    }
}
