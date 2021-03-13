using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox
{
    /// <summary>
    /// Represents a script file which content is the unaltered literal user scripts.
    /// </summary>
    public class LiteralScript : IScript
    {
        private string _baseScriptFileName;
        private readonly IList<string> _commands;

        private readonly Func<string, byte[], CancellationToken, Task> _writeAllBytesAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralScript"/> class.
        /// </summary>
        /// <param name="commands">
        /// Literal user scripts.
        /// <para>By default this is a powershell script and supports powershell commands. 
        /// If you want to specify a different kind of script use the other constructor.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public LiteralScript(IList<string> commands)
            : this(
                  commands,
                  new PowershellTranslator(),
                  $"{DateTime.Now.Ticks}.ps1")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralScript"/> class.
        /// </summary>
        /// <param name="commands">
        /// Literal user scripts.
        /// </param>
        /// <param name="translator">
        /// Translator that will generate command which will call this script from powershell.
        /// </param>
        /// <param name="scriptFileName">
        /// The script file name including the extension that will be generated with containing user scripts.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public LiteralScript(IList<string> commands, IPowershellTranslator translator, string scriptFileName)
            : this(
                  commands,
                  translator,
                  scriptFileName,
                  FileSystemAbstractions.WriteAllBytesAsync)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalScript"/> class.
        /// This constructor is solely for testing purposes and contains framework specific classes that cannot be tested.
        /// </summary>
        /// <param name="commands">
        /// Literal user scripts.
        /// </param>
        /// <param name="translator">
        /// Translator that will generate command which will call this script from powershell.
        /// </param>
        /// <param name="scriptFileName">
        /// The script file name including the extension that will be generated with containing user scripts.
        /// </param>
        /// <param name="writeAllBytesAsync">
        /// Delegate that takes filePath, content and cancellation token as parameter and generate a new file asynchronously.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        internal LiteralScript(
            IList<string> commands,
            IPowershellTranslator translator,
            string scriptFileName,
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync)
        {
            if (commands == null || !commands.Any())
            {
                throw new ArgumentNullException(nameof(commands));
            }

            if (translator == null)
            {
                throw new ArgumentNullException(nameof(translator));
            }

            if (string.IsNullOrWhiteSpace(scriptFileName))
            {
                throw new ArgumentNullException(nameof(scriptFileName));
            }

            if (writeAllBytesAsync == null)
            {
                throw new ArgumentNullException(nameof(writeAllBytesAsync));
            }

            Translator = translator;

            _commands = commands;
            _baseScriptFileName = scriptFileName;
            _writeAllBytesAsync = writeAllBytesAsync;
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
        /// Generates a file in <see cref="IOptions.RootFilesDirectoryLocation"/> which contains all the user specified scripts."/>
        /// Points <see cref="ScriptFile"/> to the newly generated script.
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
            string filePath = Path.Combine(options.RootFilesDirectoryLocation, _baseScriptFileName);

            byte[] fileContent = new UTF8Encoding().GetBytes(string.Join(Environment.NewLine, _commands));
            await _writeAllBytesAsync(filePath, fileContent, cancellationToken);

            ScriptFile = new FileInfo(filePath);
        }
    }
}
