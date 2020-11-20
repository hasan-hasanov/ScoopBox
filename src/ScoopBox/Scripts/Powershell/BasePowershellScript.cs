using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.Scripts.Powershell
{
    public class BasePowershellScript : IScript
    {
        private string _baseScriptFileName;
        private readonly IOptions _options;
        private readonly IList<string> _commands;
        private readonly IFileSystem _fileSystem;

        public BasePowershellScript(IOptions options, IList<string> commands)
            : this(
                  options,
                  commands,
                  "BaseScript.ps1",
                  new FileSystem())
        {
        }

        public BasePowershellScript(IOptions options, IList<string> commands, string baseScriptFileName)
            : this(
                  options,
                  commands,
                  baseScriptFileName,
                  new FileSystem())
        {
        }

        private BasePowershellScript(IOptions options, IList<string> commands, string baseScriptFileName, IFileSystem fileSystem)
        {
            _options = options;
            _commands = commands;
            _baseScriptFileName = baseScriptFileName;
            _fileSystem = fileSystem;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public async Task CopyAndMaterialize(IOptions options, CancellationToken cancellationToken = default)
        {
            string filePath = _fileSystem.Path.Combine(_options.RootFilesDirectoryLocation, _baseScriptFileName);
            _fileSystem.File.Delete(filePath);

            byte[] fileContent = new UTF8Encoding().GetBytes(string.Join(Environment.NewLine, _commands));
            await File.WriteAllBytesAsync(filePath, fileContent, cancellationToken);

            ScriptFile = new FileInfo(filePath);
        }
    }
}
