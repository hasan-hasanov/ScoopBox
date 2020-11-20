using ScoopBox.Translators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.Scripts.UnMaterialized
{
    public class LiteralScript : IScript
    {
        private string _baseScriptFileName;
        private readonly IList<string> _commands;

        private readonly Action<string> _deleteFile;
        private readonly Func<string, byte[], CancellationToken, Task> _writeAllBytesAsync;

        public LiteralScript(IList<string> commands, IPowershellTranslator translator)
            : this(
                  commands,
                  translator,
                  $"{DateTime.Now.Ticks}.ps1")
        {
        }

        public LiteralScript(IList<string> commands, IPowershellTranslator translator, string baseScriptFileName)
            : this(
                  commands,
                  translator,
                  baseScriptFileName,
                  path => File.Delete(path),
                  async (path, content, token) => await File.WriteAllBytesAsync(path, content, token))
        {
        }

        internal LiteralScript(
            IList<string> commands,
            IPowershellTranslator translator,
            string baseScriptFileName,
            Action<string> deleteFile,
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync)
        {
            if (commands == null || !commands.Any())
            {
                throw new ArgumentNullException(nameof(commands));
            }

            if (translator == null)
            {
                throw new ArgumentNullException(nameof(translator));
            }

            if (string.IsNullOrWhiteSpace(baseScriptFileName))
            {
                throw new ArgumentNullException(nameof(baseScriptFileName));
            }

            if (deleteFile == null)
            {
                throw new ArgumentNullException(nameof(deleteFile));
            }

            if (writeAllBytesAsync == null)
            {
                throw new ArgumentNullException(nameof(writeAllBytesAsync));
            }

            Translator = translator;

            _commands = commands;
            _baseScriptFileName = baseScriptFileName;
            _deleteFile = deleteFile;
            _writeAllBytesAsync = writeAllBytesAsync;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public IPowershellTranslator Translator { get; }

        public async Task Process(IOptions options, CancellationToken cancellationToken = default)
        {
            string filePath = Path.Combine(options.RootFilesDirectoryLocation, _baseScriptFileName);
            _deleteFile(filePath);

            byte[] fileContent = new UTF8Encoding().GetBytes(string.Join(Environment.NewLine, _commands));
            await _writeAllBytesAsync(filePath, fileContent, cancellationToken);

            ScriptFile = new FileInfo(filePath);
        }
    }
}
