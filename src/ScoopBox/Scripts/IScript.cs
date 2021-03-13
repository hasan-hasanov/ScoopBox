using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox
{
    /// <summary>
    /// Defines a script.
    /// </summary>
    public interface IScript
    {
        /// <summary>
        /// Representation of the physical script file.
        /// </summary>
        FileSystemInfo ScriptFile { get; set; }

        /// <summary>
        /// Defines a translator to translate all the different kinds of scripts, .ps1, .cmd and .bat, to be runnable from powershell.
        /// </summary>
        IPowershellTranslator Translator { get; }

        /// <summary>
        /// Processes the script which for different scripts means different actions.
        /// </summary>
        Task Process(IOptions options, CancellationToken cancellationToken = default);
    }
}
