using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses
{
    public interface ISandboxProcess
    {
        string ProcessName { get; }

        Task StartAsync();
    }
}
