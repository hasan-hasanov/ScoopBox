using System;
using System.IO;

namespace ScoopBox
{
    public class Options : IOptions
    {
        public Options()
            : this(
                  @"C:\Users\WDAGUtilityAccount\Desktop\Sandbox\BeforeScripts",
                  @"C:\Users\WDAGUtilityAccount\Desktop\Sandbox\AfterScripts",
                  @"C:\Users\WDAGUtilityAccount\Desktop\Sandbox\PackageManagerScripts",
                  "sandbox.wsb",
                  Directory.CreateDirectory($"{Path.GetTempPath()}/{Constants.SandboxFolderName}").FullName)
        {
        }

        public Options(
            string sandboxBeforeScriptsLocation,
            string sandboxAfterScriptsLocation,
            string sandboxPackageManagerScriptsLocation,
            string sandboxConfigurationFileName,
            string rootFilesDirectoryLocation)
        {
            this.SandboxBeforeScriptsLocation = sandboxBeforeScriptsLocation ?? throw new ArgumentNullException(nameof(sandboxBeforeScriptsLocation));
            this.SandboxAfterScriptsLocation = sandboxAfterScriptsLocation ?? throw new ArgumentNullException(nameof(sandboxAfterScriptsLocation));
            this.SandboxPackageManagerScriptsLocation = sandboxPackageManagerScriptsLocation ?? throw new ArgumentNullException(nameof(sandboxPackageManagerScriptsLocation));
            this.SandboxConfigurationFileName = sandboxConfigurationFileName ?? throw new ArgumentNullException(nameof(sandboxConfigurationFileName));
            this.RootFilesDirectoryLocation = rootFilesDirectoryLocation ?? throw new ArgumentNullException(nameof(rootFilesDirectoryLocation));
        }

        public string SandboxBeforeScriptsLocation { get; }

        public string SandboxAfterScriptsLocation { get; }

        public string SandboxPackageManagerScriptsLocation { get; }

        public string SandboxConfigurationFileName { get; }

        public string RootFilesDirectoryLocation { get; }
    }
}
