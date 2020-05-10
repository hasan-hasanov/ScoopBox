using System.Diagnostics;

namespace ScoopBox
{
    public class ScoopBoxProcess
    {
        public void Run(ScoopBoxOptions options)
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

            cmd.StandardInput.WriteLine(wsb);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }
    }
}
