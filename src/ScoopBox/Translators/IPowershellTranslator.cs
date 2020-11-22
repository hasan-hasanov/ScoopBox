using System.IO;

namespace ScoopBox.Translators
{
    /// <summary>
    /// Represents a powershell translator.
    /// <para>
    /// Translators are required because currently Windows Sandbox configuration file can run only one command during startup.
    /// In order to get away from this limitation, ScoopBox generates a main script that contains every user defined script inside and commands on how to run them.
    /// For example:
    /// <code>
    ///     <para>new ExternalScript(new FileInfo(@"C:\Script1.ps1"), new PowershellTranslator())</para>
    ///     <para>new ExternalScript(new FileInfo(@"C:\Script2.cmd"), new CmdTranslator())</para>
    /// </code>
    /// </para>
    /// This piece of code will generate the following simplified Main script file.
    /// <code>
    /// <para>powershell.exe -ExecutionPolicy Bypass -File C:\Script1.ps1"</para>
    /// <para>powershell.exe "C:\notepads.bat""</para>
    /// </code>
    /// The main script then will be supplied to the Windows Configuration file to be executed during startup.
    /// </summary>
    public interface IPowershellTranslator
    {
        /// <summary>
        /// Generates the command to be supplied to the main script.
        /// </summary>
        /// <param name="file">The script file.</param>
        /// <param name="options">
        /// Enables the user to control some aspects of Windows Sandbox.
        /// </param>
        /// <returns>The translated command for the main script.</returns>
        string Translate(FileSystemInfo file, IOptions options);
    }
}
