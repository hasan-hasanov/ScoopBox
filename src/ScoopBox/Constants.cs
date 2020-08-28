using System.IO;

namespace ScoopBox
{
    public static class Constants
    {
        public static readonly string SandboxFolderName = "Sandbox";
        public static readonly string InstallerScriptName = "sandbox.ps1";
        public static readonly string SandboxScriptName = "sandbox.wsb";
        public static readonly string SandboxInstallerLocation = $@"C:\Users\WDAGUtilityAccount\Desktop\{SandboxFolderName}\{InstallerScriptName}";
        public static readonly string SandboxFilesDirectoryLocation = Directory.CreateDirectory($"{Path.GetTempPath()}/{Constants.SandboxFolderName}").FullName;
        public static readonly string SandboxConfigurationFileLocation = $"{SandboxFilesDirectoryLocation}\\{SandboxScriptName}";
    }
}
