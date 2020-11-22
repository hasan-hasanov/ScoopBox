using ScoopBox.Abstractions;
using ScoopBox.Translators;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.Scripts.Materialized
{
    /// <summary>
    /// Represents a physical script file.
    /// </summary>
    public class ExternalScript : IScript
    {
        private readonly Func<string, string, CancellationToken, Task> _copyFileToDestination;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalScript"/> class.
        /// </summary>
        /// <param name="scriptFile">
        /// The physical script file that will be executed when Windows Sandbox is booted up.
        /// </param>
        /// <param name="translator">
        /// Translator that generates the a command in order this script to be launched from powershell.
        /// </param>
        public ExternalScript(FileSystemInfo scriptFile, IPowershellTranslator translator)
            : this(
                 scriptFile,
                 translator,
                 FileSystemAbstractions.CopyFileToDestination)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalScript"/> class.
        /// This constructor is solely for testing purposes and contains framework specific classes that cannot be tested.
        /// </summary>
        /// <param name="scriptFile">
        /// The physical script file that will be executed when Windows Sandbox is booted up.
        /// </param>
        /// <param name="translator">
        /// Translator that generates the a command in order this script to be launched from powershell.
        /// </param>
        /// <param name="copyFileToDestination">
        /// Delegate that takes source and destination as parameter and copies source to destination.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        internal ExternalScript(
            FileSystemInfo scriptFile,
            IPowershellTranslator translator,
            Func<string, string, CancellationToken, Task> copyFileToDestination)
        {
            if (scriptFile == null)
            {
                throw new ArgumentNullException(nameof(scriptFile));
            }

            if (translator == null)
            {
                throw new ArgumentNullException(nameof(translator));
            }

            if (copyFileToDestination == null)
            {
                throw new ArgumentNullException(nameof(copyFileToDestination));
            }

            ScriptFile = scriptFile;
            Translator = translator;

            _copyFileToDestination = copyFileToDestination;
        }

        /// <summary>
        /// Gets or sets the physical script file that will be executed when Windows Sandbox is booted up.
        /// </summary>
        public FileSystemInfo ScriptFile { get; set; }

        /// <summary>
        /// Gets the powershell translator.
        /// </summary>
        public IPowershellTranslator Translator { get; }

        /// <summary>
        /// Copies the external script to the <see cref="IOptions.RootFilesDirectoryLocation"/> and sets the <see cref="ScriptFile"/> to point to the new script.
        /// </summary>
        /// <param name="options">
        /// Enables the user to control some aspects of Windows Sandbox.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the operation.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when options is null.</exception>
        public async Task Process(IOptions options, CancellationToken cancellationToken = default)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            string sandboxScriptPath = Path.Combine(options.RootFilesDirectoryLocation, Path.GetFileName(ScriptFile.Name));
            await _copyFileToDestination(ScriptFile.FullName, sandboxScriptPath, cancellationToken);

            ScriptFile = new FileInfo(sandboxScriptPath);
        }
    }
}
