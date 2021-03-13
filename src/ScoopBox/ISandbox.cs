using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox
{
    /// <summary>
    /// Defines a single instance of Windows Sandbox.
    /// </summary>
    /// <remarks>
    /// <para>This instance currently support two package managers <see cref="ScoopPackageManagerScript"/> and <see cref="ChocolateyPackageManagerScript"/>.</para>
    /// <para>All scripts are copied or generated in a temp folder in user's temp directory.</para>
    /// <para>The types of scripts are:</para>
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="ExternalScript"/></term>
    /// <description>Represents script files with extensions: <c>ps1</c>, <c>cmd</c>, <c>bat</c></description>
    /// </item>
    /// <item>
    /// <term><see cref="LiteralScript"/></term>
    /// <description>Represents scripts as basic strings.</description>
    /// </item>
    /// <item>
    /// <term><see cref="IPackageManagerScript"/></term>
    /// <description>Represents package managers to install applications.</description>
    /// </item>
    /// </list>
    /// </remarks>
    public interface ISandbox
    {
        /// <summary>Starts a windows sandbox virtual machine and executes the script.</summary>
        /// <param name="script">
        /// The script to execute in windows sandbox.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the operation.
        /// </param>
        Task Run(IScript script, CancellationToken cancellationToken = default);

        /// <summary>Starts a windows sandbox virtual machine and executes the scripts.</summary>
        /// <param name="scripts">
        /// Scripts to be executed in windows sandbox. 
        /// The scripts are executed in order they are provided.
        /// Scripts execution is synchronous operation.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the operation.
        /// </param>
        Task Run(List<IScript> scripts, CancellationToken cancellationToken = default);
    }
}
