using System.Collections.Generic;
using System.IO;

namespace ScoopBox
{
    /// <summary>
    /// Defines a configuraiton parameters related to sandbox configuration files and scripts location.
    /// This file is the mutable variant of <see cref="IOptions"/>.
    /// </summary>
    public class Options : IOptions
    {
        /// <summary>
        /// Gets the path of Windows Sandbox desktop.
        /// Currently it is "C:\Users\WDAGUtilityAccount\Desktop\
        /// </summary>
        public string SandboxDesktopLocation => @"C:\Users\WDAGUtilityAccount\Desktop\";

        /// <summary>
        /// Gets or sets the sandbox configuration name. 
        /// <para>The default is sandbox.wsb</para>
        /// </summary>
        public string SandboxConfigurationFileName { get; set; } = "sandbox.wsb";

        /// <summary>
        /// Gets or sets the root directory of the configuration file and scripts.
        /// This directory by default is a temp directory which is shared with Windows Sandbox.
        /// <para>The default is %USERPROFILE%\AppData\Local\Temp\Sandbox</para>
        /// </summary>
        public string RootFilesDirectoryLocation { get; set; } = $"{Path.GetTempPath()}Sandbox";

        /// <summary>
        /// Gets or sets the root directory of the configuration file and scripts which is located inside the Windows Sandbox.
        /// This directory is the same as <see cref="Options.RootFilesDirectoryLocation"/> but already placed inside the Sandbox.
        /// <para>The default is C:\Users\WDAGUtilityAccount\Desktop\Sandbox</para>
        /// </summary>
        public string RootSandboxFilesDirectoryLocation { get; set; } = @"C:\Users\WDAGUtilityAccount\Desktop\Sandbox\";

        /// <summary>
        /// Gets or sets the virtualized GPU. If vGPU is disabled, the sandbox will use Windows Advanced Rasterization Platform (WARP).
        /// <para>The default is Disabled</para>
        /// </summary>
        public VGpuOptions VGpu { get; set; } = VGpuOptions.Disabled;

        /// <summary>
        /// Gets or sets the network access within the sandbox.
        /// <para>The default is Default which means enabled.</para>
        /// <para>If you disable the network access the package managers will not be able to install the desired applications.</para>
        /// </summary>
        public NetworkingOptions Networking { get; set; } = NetworkingOptions.Default;

        /// <summary>
        /// Gets or sets sharing of folders from the host with read or write permissions. 
        /// <para>Note that exposing host directories may allow malicious software to affect the system or steal data.</para>
        /// <para>The default is <see cref="Options.RootFilesDirectoryLocation"/> is mapped to <see cref="Options.RootSandboxFilesDirectoryLocation"/> </para>
        /// </summary>
        public IEnumerable<MappedFolder> UserMappedDirectories { get; set; } = new List<MappedFolder>();

        /// <summary>
        /// Gets or sets the host's microphone input into the sandbox.
        /// <para>The default is Default which means enabled.</para>
        /// </summary>
        public AudioInputOptions AudioInput { get; set; } = AudioInputOptions.Default;

        /// <summary>
        /// Gets or sets the host's webcam input into the sandbox.
        /// <para>The default is Default which means disabled.</para>
        /// </summary>
        public VideoInputOptions VideoInput { get; set; } = VideoInputOptions.Default;

        /// <summary>
        /// Gets or sets the increased security settings on the RDP session to the sandbox.
        /// <para>The default is Default which means disabled.</para>
        /// </summary>
        public ProtectedClientOptions ProtectedClient { get; set; } = ProtectedClientOptions.Default;

        /// <summary>
        /// Gets or sets the printers from the host into the sandbox.
        /// <para>The default is Default which means disabled.</para>
        /// </summary>
        public PrinterRedirectionOptions PrinterRedirection { get; set; } = PrinterRedirectionOptions.Default;

        /// <summary>
        /// Gets or sets the host clipboard with the sandbox so that text and files can be pasted back and forth.
        /// <para>The default is Default which means enabled.</para>
        /// </summary>
        public ClipboardRedirectionOptions ClipboardRedirection { get; set; } = ClipboardRedirectionOptions.Default;

        /// <summary>
        /// The amount of memory, in megabytes, to assign to the sandbox.
        /// <para>If no value is provided it will increase to the minimum amount necessary.</para>
        /// </summary>
        public int MemoryInMB { get; set; } = 0;
    }
}
