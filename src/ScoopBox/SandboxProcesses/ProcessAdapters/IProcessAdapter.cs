using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses.ProcessAdapters
{
    public interface IProcessAdapter
    {
        bool Start();
        Task StandardInputWriteLineAsync(string content);

        Task StandardInputFlushAsync();

        void StandardInputClose();

        void WaitForExit();
    }
}
