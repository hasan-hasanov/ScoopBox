using System.Threading.Tasks;

namespace ScoopBox.SandboxProcess.Abstract
{
    public interface IScoopBoxProcess
    {
        Task Run(ScoopBoxOptions options);
    }
}
