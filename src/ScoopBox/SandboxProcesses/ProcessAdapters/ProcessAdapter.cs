using System.Diagnostics;
using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses.ProcessAdapters
{
    public class ProcessAdapter : IProcessAdapter
    {
        private readonly Process _process;

        public ProcessAdapter()
        {
            _process = new Process();
        }

        public bool Start(string processName)
        {
            _process.StartInfo = new ProcessStartInfo()
            {
                FileName = processName,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
            };

            return _process.Start();
        }

        public async Task StandartInputWriteLine(string content)
        {
            await _process.StandardInput.WriteLineAsync(content);
            await _process.StandardInput.FlushAsync();
            _process.StandardInput.Close();
        }

        public void WaitForExit()
        {
            _process.WaitForExit();
        }
    }
}
