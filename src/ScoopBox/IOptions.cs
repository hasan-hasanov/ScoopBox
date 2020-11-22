using ScoopBox.Entities;
using ScoopBox.Enums;
using System.Collections.Generic;

namespace ScoopBox
{
    /// <summary>
    /// Defines a configuraiton parameters related to sandbox configuration files and scripts location.
    /// This file is intended to be immutable.
    /// </summary>
    public interface IOptions
    {
        /// <summary>
        /// Gets the path of Windows Sandbox desktop.
        /// Currently it is "C:\Users\WDAGUtilityAccount\Desktop\
        /// </summary>
        string SandboxDesktopLocation { get; }

        /// <summary>
        /// Gets the sandbox configuration name.
        /// </summary>
        string SandboxConfigurationFileName { get; }

        /// <summary>
        /// Gets the root directory of the configuration file and scripts.
        /// This directory by default is a temp directory which is shared with Windows Sandbox.
        /// </summary>
        string RootFilesDirectoryLocation { get; }

        /// <summary>
        /// Gets the root directory of the configuration file and scripts which is located inside the Windows Sandbox.
        /// This directory is the same as <see cref="IOptions.RootFilesDirectoryLocation"/> but already placed inside the Sandbox.
        /// </summary>
        string RootSandboxFilesDirectoryLocation { get; }

        /// <summary>
        /// Gets the virtualized GPU status. If vGPU is disabled, the sandbox will use Windows Advanced Rasterization Platform (WARP).
        /// </summary>
        VGpuOptions VGpu { get; }

        /// <summary>
        /// Gets the status of network access within the sandbox.
        /// </summary>
        NetworkingOptions Networking { get; }

        /// <summary>
        /// Gets shared folders from the host with read or write permissions. 
        /// <para>Note that exposing host directories may allow malicious software to affect the system or steal data.</para>
        /// </summary>
        IEnumerable<MappedFolder> UserMappedDirectories { get; }

        /// <summary>
        /// Gets the microphone input status.
        /// </summary>
        AudioInputOptions AudioInput { get; }

        /// <summary>
        /// Gets the webcam input staus.
        /// </summary>
        VideoInputOptions VideoInput { get; }

        /// <summary>
        /// Gets the increased security settings on the RDP session status.
        /// </summary>
        ProtectedClientOptions ProtectedClient { get; }

        /// <summary>
        /// Gets the printer input status.
        /// </summary>
        PrinterRedirectionOptions PrinterRedirection { get; }

        /// <summary>
        /// Gets the clipboard redirection status.
        /// </summary>
        ClipboardRedirectionOptions ClipboardRedirection { get; }

        /// <summary>
        /// Gets the amount of memory, in megabytes, assigned to the sandbox.
        /// </summary>
        int MemoryInMB { get; }
    }
}
