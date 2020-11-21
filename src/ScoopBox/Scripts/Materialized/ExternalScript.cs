using ScoopBox.Abstractions;
using ScoopBox.Translators;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.Scripts.Materialized
{
    public class ExternalScript : IScript
    {
        private readonly Func<string, string, CancellationToken, Task> _copyFileToDestination;

        public ExternalScript(FileSystemInfo scriptFile, IPowershellTranslator translator)
            : this(
                 scriptFile,
                 translator,
                 FileSystemAbstractions.CopyFileToDestination)
        {
        }

        internal ExternalScript(
            FileSystemInfo scriptFile,
            IPowershellTranslator translator,
            Func<string, string, CancellationToken, Task> copyFileToDestination)
        {
            if (scriptFile == null)
            {
                throw new ArgumentNullException(nameof(scriptFile));
            }

            if (translator == null)
            {
                throw new ArgumentNullException(nameof(translator));
            }

            if (copyFileToDestination == null)
            {
                throw new ArgumentNullException(nameof(copyFileToDestination));
            }

            ScriptFile = scriptFile;
            Translator = translator;

            _copyFileToDestination = copyFileToDestination;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public IPowershellTranslator Translator { get; }

        public async Task Process(IOptions options, CancellationToken cancellationToken = default)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            string sandboxScriptPath = Path.Combine(options.RootFilesDirectoryLocation, Path.GetFileName(ScriptFile.Name));
            await _copyFileToDestination(ScriptFile.FullName, sandboxScriptPath, cancellationToken);

            ScriptFile = new FileInfo(sandboxScriptPath);
        }
    }
}
