using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace ScoopBox.Scripts.Powershell
{
    public class BasePowershellScript : IScript
    {
        private readonly IOptions _options;
        private readonly IList<string> _commands;
        private readonly IFileSystem _fileSystem;

        public BasePowershellScript(IOptions options, IList<string> commands)
            : this(
                  options,
                  commands,
                  new FileSystem())
        {
        }

        private BasePowershellScript(IOptions options, IList<string> commands, IFileSystem fileSystem)
        {
            _options = options;
            _commands = commands;
            _fileSystem = fileSystem;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public virtual Task GenerateScript()
        {
            string filePath = _fileSystem.Path.Combine(_options.RootFilesDirectoryLocation, "BaseScript.ps1");

            _fileSystem.File.Delete(filePath);
            _fileSystem.File.WriteAllText(filePath, string.Join(Environment.NewLine, _commands));

            ScriptFile = new FileInfo(filePath);

            return Task.CompletedTask;
        }
    }
}
