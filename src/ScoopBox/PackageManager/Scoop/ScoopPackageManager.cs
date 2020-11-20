using ScoopBox.Scripts;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.PackageManager.Scoop
{
    public class ScoopPackageManager : IPackageManager, IScript
    {
        private string _packageManagerScriptName;
        private readonly IFileSystem _fileSystem;
        private readonly StringBuilder _sbScoopPackageManagerBuilder;

        public ScoopPackageManager(IEnumerable<string> applications)
            : this(applications, $"{nameof(ScoopPackageManager)}.ps1")
        {
        }

        public ScoopPackageManager(IEnumerable<string> applications, string scriptName)
            : this(applications, scriptName, new FileSystem())
        {
        }

        private ScoopPackageManager(IEnumerable<string> applications, string scriptName, IFileSystem fileSystem)
        {
            _packageManagerScriptName = scriptName;
            _fileSystem = fileSystem;

            Applications = applications;
            _sbScoopPackageManagerBuilder = new StringBuilder();
        }

        public FileSystemInfo ScriptFile { get; set; }

        public IEnumerable<string> Applications { get; }

        public async Task CopyAndMaterialize(IOptions options, CancellationToken cancellationToken = default)
        {
            _sbScoopPackageManagerBuilder.AppendLine("Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')");
            _sbScoopPackageManagerBuilder.AppendLine("scoop install git");
            _sbScoopPackageManagerBuilder.AppendLine("scoop bucket add extras");
            _sbScoopPackageManagerBuilder.Append("scoop install").Append(" ").AppendLine(string.Join(" ", Applications));

            string fullScriptPath = Path.Combine(options.RootFilesDirectoryLocation, _packageManagerScriptName);
            byte[] content = new UTF8Encoding().GetBytes(_sbScoopPackageManagerBuilder.ToString());

            await File.WriteAllBytesAsync(fullScriptPath, content, cancellationToken);

            ScriptFile = new FileInfo(fullScriptPath);
        }
    }
}
