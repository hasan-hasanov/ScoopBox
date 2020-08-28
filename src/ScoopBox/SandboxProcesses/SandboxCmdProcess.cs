using System.Diagnostics;
using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses
{
    public class SandboxCmdProcess : ISandboxProcess
    {
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

            await cmd.StandardInput.WriteLineAsync($"\"{Constants.SandboxConfigurationFileLocation}\"");
            await cmd.StandardInput.FlushAsync();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }
    }
}
