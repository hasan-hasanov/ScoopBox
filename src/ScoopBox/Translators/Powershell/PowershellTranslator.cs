using System;
using System.IO;
using System.Text;

namespace ScoopBox.Translators.Powershell
{
    public class PowershellTranslator : IPowershellTranslator
    {
        private readonly string[] _argumentsAfter;

        public PowershellTranslator()
            : this(null)
        {
        }

        public PowershellTranslator(string[] argumentsAfter)
        {
            _argumentsAfter = argumentsAfter;
        }

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
                .Append($@"powershell.exe -ExecutionPolicy Bypass -File { sandboxScriptFileFullName } 3>&1 2>&1 > ""{Path.Combine(options.SandboxDesktopLocation, "Log.txt")}""");

            if (_argumentsAfter?.Length > 0)
            {
                sbPowershellCommandBuilder.Append(" ").Append(string.Join(" ", _argumentsAfter));
            }

            return sbPowershellCommandBuilder.ToString();
        }
    }
}
