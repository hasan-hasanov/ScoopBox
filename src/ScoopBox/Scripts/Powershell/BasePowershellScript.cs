using System;
using System.Collections.Generic;
using System.IO;
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

        private readonly Action<string> _deleteFile;
        private readonly Func<string, byte[], CancellationToken, Task> _writeAllBytesAsync;

        public BasePowershellScript(IOptions options, IList<string> commands)
            : this(
                  options,
                  commands,
                  "BaseScript.ps1")
        {
        }

        public BasePowershellScript(IOptions options, IList<string> commands, string baseScriptFileName)
            : this(
                  options,
                  commands,
                  baseScriptFileName,
                  path => File.Delete(path),
                  async (path, content, token) => await File.WriteAllBytesAsync(path, content, token))
        {
        }

        internal BasePowershellScript(
            IOptions options,
            IList<string> commands,
            string baseScriptFileName,
            Action<string> deleteFile,
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync)
        {
            _options = options;
            _commands = commands;
            _baseScriptFileName = baseScriptFileName;
            _deleteFile = deleteFile;
            _writeAllBytesAsync = writeAllBytesAsync;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public async Task CopyOrMaterialize(IOptions options, CancellationToken cancellationToken = default)
        {
            string filePath = Path.Combine(_options.RootFilesDirectoryLocation, _baseScriptFileName);
            _deleteFile(filePath);

            byte[] fileContent = new UTF8Encoding().GetBytes(string.Join(Environment.NewLine, _commands));
            await _writeAllBytesAsync(filePath, fileContent, cancellationToken);

            ScriptFile = new FileInfo(filePath);
        }
    }
}
