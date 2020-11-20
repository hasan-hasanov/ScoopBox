using System.IO;
using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.Scripts
{
    public class Script : IScript
    {
        private readonly IFileSystem _fileSystem;

        public Script(FileSystemInfo scriptFile)
            : this(scriptFile, new FileSystem())
        {
        }

        private Script(FileSystemInfo fileStream, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            ScriptFile = fileStream;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public async Task CopyAndMaterialize(IOptions options, CancellationToken cancellationToken = default)
        {
            string sandboxScriptPath = Path.Combine(options.RootFilesDirectoryLocation, Path.GetFileName(ScriptFile.Name));

            using (FileStream sourceStream = File.Open(ScriptFile.FullName, FileMode.Open))
            using (FileStream destinationStream = File.Create(sandboxScriptPath))
            {
                await sourceStream.CopyToAsync(destinationStream, cancellationToken);
            }

            ScriptFile = new FileInfo(sandboxScriptPath);
        }
    }
}
