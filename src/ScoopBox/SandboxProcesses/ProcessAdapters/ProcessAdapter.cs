using System.Diagnostics;
using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses.ProcessAdapters
{
    public class ProcessAdapter : IProcessAdapter
    {
        private readonly Process _process;

        public ProcessAdapter(string processName)
        {
            _process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = processName,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                }
            };
        }

        public bool Start()
        {
            return _process.Start();
        }

        public async Task StandardInputWriteLineAsync(string content)
        {
            await _process.StandardInput.WriteLineAsync(content);
        }

        public async Task StandardInputFlushAsync()
        {
            await _process.StandardInput.FlushAsync();
        }

        public void StandardInputClose()
        {
            _process.StandardInput.Close();
        }

        public void WaitForExit()
        {
            _process.WaitForExit();
        }
    }
}
