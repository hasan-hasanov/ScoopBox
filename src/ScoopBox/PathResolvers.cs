using System.IO;

namespace ScoopBox
{
    public static class PathResolvers
    {
        public static string GetBeforeScriptsPath(string rootFilesDirectoryLocation)
        {
            return Directory.CreateDirectory($"{Path.Combine(rootFilesDirectoryLocation, "BeforeScripts")}").FullName;
        }

        public static string GetAfterScriptsPath(string rootFilesDirectoryLocation)
        {
            return Directory.CreateDirectory($"{Path.Combine(rootFilesDirectoryLocation, "AfterScripts")}").FullName;
        }

        public static string GetPackageManagerScriptsPath(string rootFilesDirectoryLocation)
        {
            return Directory.CreateDirectory($"{Path.Combine(rootFilesDirectoryLocation, "PackageManagerScripts")}").FullName;
        }
    }
}
