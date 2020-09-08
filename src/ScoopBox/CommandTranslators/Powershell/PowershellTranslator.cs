using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScoopBox.CommandTranslators.Powershell
{
    public class PowershellTranslator : ICommandTranslator
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

        public string Translate(FileSystemInfo file, string rootSandboxScriptFilesLocation)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (string.IsNullOrWhiteSpace(rootSandboxScriptFilesLocation))
            {
                throw new ArgumentNullException(nameof(rootSandboxScriptFilesLocation));
            }

            string sandboxScriptFileFullName = Path.Combine(rootSandboxScriptFilesLocation, file.Name);
            StringBuilder sbPowershellCommandBuilder = new StringBuilder().Append($"powershell.exe -ExecutionPolicy Bypass -File { sandboxScriptFileFullName}");

            if (_argumentsAfter?.Length > 0)
            {
                sbPowershellCommandBuilder.Append(" ").Append(string.Join(" ", _argumentsAfter));
            }

            return sbPowershellCommandBuilder.ToString();
        }
    }
}
