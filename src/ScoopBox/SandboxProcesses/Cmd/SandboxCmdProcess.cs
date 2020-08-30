using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses.Cmd
{
    public class SandboxCmdProcess : ISandboxProcess
    {
        private readonly string configurationFileLocation;

        public SandboxCmdProcess()
            : this(
                  $"{Path.GetTempPath()}/Sandbox",
                  "sandbox.wsb")
        {
        }

        public SandboxCmdProcess(string rootFilesDirectoryLocation, string sandboxConfigurationFileName)
        {
            if (string.IsNullOrWhiteSpace(rootFilesDirectoryLocation))
            {
                throw new ArgumentNullException(nameof(rootFilesDirectoryLocation));
            }

            if (string.IsNullOrWhiteSpace(sandboxConfigurationFileName))
            {
                throw new ArgumentNullException(nameof(sandboxConfigurationFileName));
            }

            configurationFileLocation = Path.Combine(rootFilesDirectoryLocation, sandboxConfigurationFileName);
        }

        public async Task StartAsync()
        {
            Process cmd = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                }
            };

            cmd.Start();

            await cmd.StandardInput.WriteLineAsync($"\"{configurationFileLocation}\"");
            await cmd.StandardInput.FlushAsync();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }
    }
}
