using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses
{
    public interface ISandboxProcess
    {
        Task StartAsync();
    }
}
