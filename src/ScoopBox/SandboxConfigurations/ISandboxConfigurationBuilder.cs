using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.SandboxConfigurations
{
    /// <summary>
    /// Defines an instance of Windows Sandbox Configuration Builder.
    /// </summary>
    public interface ISandboxConfigurationBuilder
    {
        /// <summary>
        /// Builds the Windows Sandbox configuration file.
        /// <paramref name="logonCommand">
        /// A command that will be executed when Windows Sandbox booted up.
        /// </paramref>
        /// <paramref name="cancellationToken">
        /// A cancellation token that can be used to cancel the operation.
        /// </paramref>
        /// </summary>
        Task Build(string logonCommand, CancellationToken cancellationToken = default);
    }
}
