using ScoopBox.Abstractions;
using System;
using System.IO;
using System.Text;

namespace ScoopBox.Translators.Cmd
{
    /// <summary>
    /// Represents a .cmd translator.
    /// Generates command that can run .cmd files from powershell.
    /// </summary>
    public class CmdTranslator : IPowershellTranslator
    {
        private readonly string[] _argumentsAfter;
        private readonly Func<long> _getTicks;

        /// <summary>
        /// Initializes a new instance of the <see cref="CmdTranslator"/> class.
        /// </summary>
        public CmdTranslator()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CmdTranslator"/> class.
        /// </summary>
        /// <param name="argumentsAfter">
        /// Arguments that will be appended at the end for the generated command
        /// </param>
        public CmdTranslator(string[] argumentsAfter)
            : this(argumentsAfter, DateTimeAbstractions.GetTicks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CmdTranslator"/> class.
        /// This constructor is solely for testing purposes and contains framework specific classes that cannot be tested.
        /// </summary>
        /// <param name="argumentsAfter">
        /// Arguments that will be appended at the end for the generated command
        /// </param>
        /// <param name="getTicks">
        /// Delegate that returns DateTime.Now.Ticks which is used to generate uniqe log file names.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when getTicks is null</exception>
        internal CmdTranslator(string[] argumentsAfter, Func<long> getTicks)
        {
            if (getTicks == null)
            {
                throw new ArgumentNullException(nameof(getTicks));
            }

            _argumentsAfter = argumentsAfter;
            _getTicks = getTicks;
        }

        /// <summary>
        /// Generates the command to be supplied to the main script.
        /// Since there is no visibility from Windows Sandbox wether a script is successfull or not this method also generates log files in the desktop.
        /// The user should see a configuration file generated for every script provided.
        /// This configuration file is not necesserily contains something.
        /// </summary>
        /// <param name="file">
        /// The script file.
        /// </param>
        /// <param name="options">
        /// Enables the user to control some aspects of Windows Sandbox.
        /// </param>
        /// <returns>The translated command for the main script.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any of the parameters are null.
        /// </exception>
        public string Translate(FileSystemInfo file, IOptions options)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            string sandboxScriptFileFullName = Path.Combine(options.RootSandboxFilesDirectoryLocation, file.Name);

            StringBuilder sbPowershellCommandBuilder = new StringBuilder()
                .Append($@"powershell.exe ""{ sandboxScriptFileFullName }""")
                .Append(" ")
                .Append($@"3>&1 2>&1 > ""{Path.Combine(options.SandboxDesktopLocation, $"Log_{_getTicks()}.txt")}""");

            if (_argumentsAfter?.Length > 0)
            {
                sbPowershellCommandBuilder.Append(" ").Append(string.Join(" ", _argumentsAfter));
            }

            return sbPowershellCommandBuilder.ToString();
        }
    }
}
