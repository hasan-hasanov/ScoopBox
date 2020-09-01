using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading.Tasks;

namespace ScoopBox.PackageManager.Scoop
{
    public class ScoopPackageManager : IPackageManager
    {
        private readonly StringBuilder _sbScoopPackageManagerBuilder;
        private readonly IFileSystem _fileSystem;

        public string PackageManagerScriptName { get; }

        public IEnumerable<string> Applications { get; }

        public ScoopPackageManager(IEnumerable<string> applications)
            : this($"{nameof(ScoopPackageManager)}.ps1", applications)
        {
        }

        public ScoopPackageManager(string scriptName, IEnumerable<string> applications)
            : this(scriptName, applications, new FileSystem())
        {
        }

        public ScoopPackageManager(string scriptName, IEnumerable<string> applications, IFileSystem fileSystem)
        {
            PackageManagerScriptName = scriptName ?? throw new ArgumentNullException(nameof(scriptName));
            Applications = applications ?? throw new ArgumentNullException(nameof(applications));

            _sbScoopPackageManagerBuilder = new StringBuilder();
            _fileSystem = fileSystem;
        }

        public async Task<string> GenerateScriptFile(string location)
        {
            BuildDownloader();
            BuildGitInstaller();
            BuildExtrasBucketAdder();
            BuildApplicationInstaller();

            string content = _sbScoopPackageManagerBuilder.ToString();
            string fullScriptPath = Path.Combine(location, PackageManagerScriptName);
            using (StreamWriter writer = _fileSystem.File.CreateText(fullScriptPath))
            {
                await writer.WriteAsync(content);
            }

            return fullScriptPath;
        }

        private void BuildDownloader()
        {
            _sbScoopPackageManagerBuilder.AppendLine("Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')");
        }

        private void BuildGitInstaller()
        {
            _sbScoopPackageManagerBuilder.AppendLine("scoop install git");
        }

        private void BuildExtrasBucketAdder()
        {
            _sbScoopPackageManagerBuilder.AppendLine("scoop bucket add extras");
        }

        private void BuildApplicationInstaller()
        {
            _sbScoopPackageManagerBuilder
                .Append("scoop install")
                .Append(" ")
                .AppendLine(string.Join(" ", Applications));
        }
    }
}
