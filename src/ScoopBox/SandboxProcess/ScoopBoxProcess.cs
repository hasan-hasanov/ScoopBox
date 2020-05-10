using ScoopBox.SandboxProcess.Abstract;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ScoopBox.SandboxProcess
{
    public class ScoopBoxProcess : IScoopBoxProcess
    {
        public async Task Run(ScoopBoxOptions options)
        {
            string wsb = $"{options.UserFilesPath}\\{Constants.SandboxScriptName}";

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

            await cmd.StandardInput.WriteLineAsync($"\"{wsb}\"");
            await cmd.StandardInput.FlushAsync();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }
    }
}
