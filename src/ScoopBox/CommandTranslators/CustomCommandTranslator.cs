using System;
using System.Collections.Generic;
using System.IO;

namespace ScoopBox.CommandTranslators
{
    public class CustomCommandTranslator : ICommandTranslator
    {
        private readonly string _command;

        public CustomCommandTranslator(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException(nameof(command));
            }

            _command = command;
        }

        public string Translate(FileSystemInfo file, string rootSandboxScriptFilesLocation)
        {
            string sandboxScriptFileFullName = Path.Combine(rootSandboxScriptFilesLocation, file.Name);

            try
            {
                string fullCommand = string.Format(_command, sandboxScriptFileFullName);
                return fullCommand;
            }
            catch (Exception)
            {
                return _command;
            }
        }
    }
}
