using System.IO;

namespace ScoopBox
{
    public static class Constants
    {
        public static readonly string SandboxFolder = "Sandbox";
        public static readonly string InstallerName = "sandbox.ps1";
        public static readonly string SandboxName = "sandbox.wsb";
        public static readonly string SandboxDesktopLocation = $@"C:\Users\WDAGUtilityAccount\Desktop\{SandboxFolder}";
        public static readonly string SandboxInstallerLocation = $@"C:\Users\WDAGUtilityAccount\Desktop\{SandboxFolder}\{InstallerName}";
    }
}
