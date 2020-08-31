using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses.ProcessAdapters
{
    public interface IProcessAdapter
    {
        bool Start(string processName);

        Task StandartInputWriteLine(string content);

        void WaitForExit();
    }
}
