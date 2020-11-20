using ScoopBox.Translators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ScoopBox.Test")]
namespace ScoopBox.Scripts.PackageManagers.Scoop
{
    public class ScoopPackageManager : IPackageManager, IScript
    {
        private string _packageManagerScriptName;
        private readonly StringBuilder _sbScoopPackageManagerBuilder;
        private readonly Func<string, byte[], CancellationToken, Task> _writeAllBytesAsync;

        public ScoopPackageManager(IEnumerable<string> applications, IPowershellTranslator translator)
            : this(
                  applications,
                  translator,
                  $"{nameof(ScoopPackageManager)}.ps1")
        {
        }

        public ScoopPackageManager(
            IEnumerable<string> applications,
            IPowershellTranslator translator,
            string scriptName)
            : this(
                  applications,
                  translator,
                  scriptName,
                  new StringBuilder(),
                  async (path, content, token) => await File.WriteAllBytesAsync(path, content, token))
        {
        }

        internal ScoopPackageManager(
            IEnumerable<string> applications,
            IPowershellTranslator translator,
            string packageManagerScriptName,
            StringBuilder sbScoopPackageManagerBuilder,
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync)
        {
            if (applications == null || !applications.Any())
            {
                throw new ArgumentNullException(nameof(applications));
            }

            if (translator == null)
            {
                throw new ArgumentNullException(nameof(translator));
            }

            if (string.IsNullOrWhiteSpace(packageManagerScriptName))
            {
                throw new ArgumentNullException(nameof(packageManagerScriptName));
            }

            if (sbScoopPackageManagerBuilder == null)
            {
                throw new ArgumentNullException(nameof(sbScoopPackageManagerBuilder));
            }

            if (writeAllBytesAsync == null)
            {
                throw new ArgumentNullException(nameof(writeAllBytesAsync));
            }

            Applications = applications;
            Translator = translator;

            _packageManagerScriptName = packageManagerScriptName;
            _sbScoopPackageManagerBuilder = sbScoopPackageManagerBuilder;
            _writeAllBytesAsync = writeAllBytesAsync;
        }

        public FileSystemInfo ScriptFile { get; set; }

        public IEnumerable<string> Applications { get; }

        public IPowershellTranslator Translator { get; }

        public async Task CopyOrMaterialize(IOptions options, CancellationToken cancellationToken = default)
        {
            _sbScoopPackageManagerBuilder.AppendLine("Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')");
            _sbScoopPackageManagerBuilder.AppendLine("scoop install git");
            _sbScoopPackageManagerBuilder.AppendLine("scoop bucket add extras");
            _sbScoopPackageManagerBuilder.Append("scoop install").Append(" ").AppendLine(string.Join(" ", Applications));

            string fullScriptPath = Path.Combine(options.RootFilesDirectoryLocation, _packageManagerScriptName);
            byte[] content = new UTF8Encoding().GetBytes(_sbScoopPackageManagerBuilder.ToString());

            await _writeAllBytesAsync(fullScriptPath, content, cancellationToken);

            ScriptFile = new FileInfo(fullScriptPath);
        }
    }
}
