using System.Diagnostics;
using System.Threading.Tasks;

namespace ScoopBox
{
    internal class ProcessAbstractions
    {
        public static async Task StartCmdWithInput(string input)
        {
            var process = new Process()
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

            process.Start();
            await process.StandardInput.WriteLineAsync($"\"{input}\"");
            await process.StandardInput.FlushAsync();
            process.StandardInput.Close();
            process.WaitForExit();
        }
    }
}
