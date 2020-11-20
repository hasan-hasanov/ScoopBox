using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.SandboxConfigurations
{
    public interface ISandboxConfigurationBuilder
    {
        Task Build(string logonCommand, CancellationToken cancellationToken = default);
    }
}
