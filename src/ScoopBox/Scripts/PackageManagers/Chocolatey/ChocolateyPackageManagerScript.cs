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
    /// Represents Chocolatey package manager.
    /// This script is generated automatically based on user input.
    /// </summary>
    public class ChocolateyPackageManagerScript : IPackageManagerScript
    {
        private string _packageManagerScriptName;
        private readonly StringBuilder _sbChocolatePackageManagerBuilder;
        private readonly Func<string, byte[], CancellationToken, Task> _writeAllBytesAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChocolateyPackageManagerScript"/> class.
        /// </summary>
        /// <param name="applications">
        /// Applications that will be installed using this package manager.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when applications is null.</exception>
        public ChocolateyPackageManagerScript(IEnumerable<string> applications)
            : this(
                  applications,
                  new PowershellTranslator(),
                  $"{nameof(ChocolateyPackageManagerScript)}.ps1")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChocolateyPackageManagerScript"/> class.
        /// </summary>
        /// <param name="applications">
        /// Applications that will be installed using this package manager.
        /// </param>
        /// <param name="translator">
        /// Translator that will used to generate command that will be run from powershell.
        /// <para>
        /// The default translate is <see cref="PowershellTranslator"/>.
        /// </para>
        /// <para>
        /// This constructor is defined solely if the user has defined custom translator using <see cref="IPowershellTranslator"/>.
        /// </para>
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public ChocolateyPackageManagerScript(IEnumerable<string> applications, IPowershellTranslator translator)
            : this(
                  applications,
                  translator,
                  $"{nameof(ChocolateyPackageManagerScript)}.ps1")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChocolateyPackageManagerScript"/> class.
        /// </summary>
        /// <param name="applications">
        /// Applications that will be installed using this package manager.
        /// </param>
        /// <param name="translator">
        /// Translator that will used to generate command that will be run from powershell.
        /// <para>
        /// The default translate is <see cref="PowershellTranslator"/>.
        /// </para>
        /// <para>
        /// This constructor is defined solely if the user has defined custom translator using <see cref="IPowershellTranslator"/>.
        /// </para>
        /// </param>
        /// <param name="scriptName">
        /// The name of the script that will be generated.
        /// <para>The default is ChocolateyPackageManagerScript.ps1</para>
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public ChocolateyPackageManagerScript(
            IEnumerable<string> applications,
            IPowershellTranslator translator,
            string scriptName)
            : this(
                  applications,
                  translator,
                  scriptName,
                  new StringBuilder(),
                  FileSystemAbstractions.WriteAllBytesAsync)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChocolateyPackageManagerScript"/> class.
        /// This constructor is solely for testing purposes and contains framework specific classes that cannot be tested.
        /// </summary>
        /// <param name="applications">
        /// Applications that will be installed using this package manager.
        /// </param>
        /// <param name="translator">
        /// Translator that will used to generate command that will be run from powershell.
        /// <para>
        /// The default translate is <see cref="PowershellTranslator"/>.
        /// </para>
        /// <para>
        /// This constructor is defined solely if the user has defined custom translator using <see cref="IPowershellTranslator"/>.
        /// </para>
        /// </param>
        /// <param name="scriptName">
        /// The name of the script that will be generated.
        /// <para>The default is ChocolateyPackageManagerScript.ps1</para>
        /// </param>
        /// <param name="writeAllBytesAsync">
        /// Delegate that takes filePath, content and cancellation token as parameter and generate a new file asynchronously.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        internal ChocolateyPackageManagerScript(
            IEnumerable<string> applications,
            IPowershellTranslator translator,
            string packageManagerScriptName,
            StringBuilder sbChocolateyPackageManagerBuilder,
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync)
        {
            if (applications == null || !applications.Any())
            {
                throw new ArgumentNullException(nameof(applications));
            }

            if (translator == null)
            {
                throw new ArgumentNullException(nameof(translator));
            }

            if (string.IsNullOrWhiteSpace(packageManagerScriptName))
            {
                throw new ArgumentNullException(nameof(packageManagerScriptName));
            }

            if (sbChocolateyPackageManagerBuilder == null)
            {
                throw new ArgumentNullException(nameof(sbChocolateyPackageManagerBuilder));
            }

            if (writeAllBytesAsync == null)
            {
                throw new ArgumentNullException(nameof(writeAllBytesAsync));
            }

            Applications = applications;
            Translator = translator;

            _packageManagerScriptName = packageManagerScriptName;
            _sbChocolatePackageManagerBuilder = sbChocolateyPackageManagerBuilder;
            _writeAllBytesAsync = writeAllBytesAsync;
        }

        /// <summary>
        /// Gets or sets the generated script file.
        /// </summary>
        public FileSystemInfo ScriptFile { get; set; }

        /// <summary>
        /// Gets the applications that will be installed using this package manager.
        /// </summary>
        public IEnumerable<string> Applications { get; }

        /// <summary>
        /// Gets the translator that will be used to generate powershell command.
        /// </summary>
        public IPowershellTranslator Translator { get; }

        /// <summary>
        /// Generates a new script in <see cref="IOptions.RootFilesDirectoryLocation"/>.
        /// Points <see cref="ScriptFile"/> to the newly generated script.
        /// The script installs chocolatey package manager to Windows Sandbox and the <see cref="Applications"/>.
        /// </summary>
        /// <param name="options">
        /// Enables the user to control some aspects of Windows Sandbox.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the operation.
        /// </param>
        public async Task Process(IOptions options, CancellationToken cancellationToken = default)
        {
            _sbChocolatePackageManagerBuilder.AppendLine(@"Write-Host Start executing chocolatey package manager");
            _sbChocolatePackageManagerBuilder.AppendLine("Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1')");
            _sbChocolatePackageManagerBuilder.AppendLine("choco feature enable -n allowGlobalConfirmation");
            _sbChocolatePackageManagerBuilder.Append("choco install").Append(" ").AppendLine(string.Join(" ", Applications));
            _sbChocolatePackageManagerBuilder.AppendLine(@"Write-Host Finished executing chocolatey package manager");

            string fullScriptPath = Path.Combine(options.RootFilesDirectoryLocation, _packageManagerScriptName);
            byte[] content = new UTF8Encoding().GetBytes(_sbChocolatePackageManagerBuilder.ToString());

            await _writeAllBytesAsync(fullScriptPath, content, cancellationToken);

            ScriptFile = new FileInfo(fullScriptPath);
        }
    }
}
