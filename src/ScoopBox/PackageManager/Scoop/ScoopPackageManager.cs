using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScoopBox.PackageManager.Scoop
{
    public class ScoopPackageManager : IPackageManager
    {
        private readonly StringBuilder _sbScoopPackageManagerBuilder;
        private readonly IEnumerable<string> _applications;

        public string PackageManagerScriptName { get; }

        public ScoopPackageManager(IEnumerable<string> applications)
            : this($"{nameof(ScoopPackageManager)}.ps1", applications)
        {
        }

        public ScoopPackageManager(string scriptName, IEnumerable<string> applications)
        {
            PackageManagerScriptName = scriptName ?? throw new ArgumentNullException(nameof(scriptName));
            _applications = applications ?? throw new ArgumentNullException(nameof(applications));

            _sbScoopPackageManagerBuilder = new StringBuilder();
        }

        public async Task<string> GenerateScriptFile(string location)
        {
            BuildDownloader();
            BuildGitInstaller();
            BuildExtrasBucketAdder();
            BuildApplicationInstaller();

            string content = _sbScoopPackageManagerBuilder.ToString();

            using (StreamWriter writer = File.CreateText(Path.Combine(location, PackageManagerScriptName)))
            {
                await writer.WriteAsync(content);
            }

            return PackageManagerScriptName;
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
                .Append(string.Join(" ", _applications));
        }
    }
}
