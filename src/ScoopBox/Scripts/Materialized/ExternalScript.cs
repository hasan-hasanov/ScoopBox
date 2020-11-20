using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.Scripts.Materialized
{
    public class ExternalScript : IScript
    {
        private readonly Func<string, string, CancellationToken, Task> _copyFileToDestination;

        public ExternalScript(FileSystemInfo scriptFile)
            : this(
                 scriptFile,
                 async (source, destination, token) =>
                 {
                     using FileStream sourceStream = File.Open(source, FileMode.Open);
                     using FileStream destinationStream = File.Create(destination);
                     await sourceStream.CopyToAsync(destinationStream, token);
                 })
        {
        }

        internal ExternalScript(
            FileSystemInfo scriptFile,
            Func<string, string, CancellationToken, Task> copyFileToDestination)
        {
            if (scriptFile == null)
            {
                throw new ArgumentNullException(nameof(scriptFile));
            }

            if (copyFileToDestination == null)
            {
                throw new ArgumentNullException(nameof(copyFileToDestination));
            }

            ScriptFile = scriptFile;

            _copyFileToDestination = copyFileToDestination;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public async Task CopyOrMaterialize(IOptions options, CancellationToken cancellationToken = default)
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
