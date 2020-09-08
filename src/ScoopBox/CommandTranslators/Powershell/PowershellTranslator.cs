using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScoopBox.CommandTranslators.Powershell
{
    public class PowershellTranslator : ICommandTranslator
    {
        private readonly string[] _argumentsBefore;
        private readonly string[] _argumentsAfter;

        public PowershellTranslator()
            : this(null, null)
        {
        }

        public PowershellTranslator(string[] argumentsBefore, string[] argumentsAfter)
        {
            _argumentsBefore = argumentsBefore;
            _argumentsAfter = argumentsAfter;
        }

        public IEnumerable<string> Translate(FileSystemInfo file, string rootSandboxScriptFilesLocation)
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
            StringBuilder sbPowershellCommandBuilder = new StringBuilder().Append("powershell.exe");

            if (_argumentsBefore?.Length > 0)
            {
                sbPowershellCommandBuilder.Append(" ").Append(string.Join(" ", _argumentsBefore));
            }

            sbPowershellCommandBuilder.Append(" ").Append(sandboxScriptFileFullName);

            if (_argumentsAfter?.Length > 0)
            {
                sbPowershellCommandBuilder.Append(" ").Append(string.Join(" ", _argumentsAfter));
            }

            string executionPolicyCommand = $"powershell.exe -ExecutionPolicy Bypass -File {sandboxScriptFileFullName}";
            string powershellCommand = sbPowershellCommandBuilder.ToString();

            return new List<string>() { executionPolicyCommand, powershellCommand };
        }
    }
}
