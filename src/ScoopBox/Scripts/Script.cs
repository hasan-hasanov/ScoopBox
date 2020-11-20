using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.Scripts
{
    public class Script : IScript
    {
        public Script(FileSystemInfo scriptFile)
        {
            ScriptFile = scriptFile;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public async Task CopyOrMaterialize(IOptions options, CancellationToken cancellationToken = default)
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
