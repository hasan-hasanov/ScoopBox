using ScoopBox.Scripts.InstallerScripts.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoopBox.Scripts.InstallerScripts
{
    public class ScoopBucketsBuilder : IScoopBucketsBuilder
    {
        public string Build(List<string> buckets)
        {
            StringBuilder extrasInstaller = new StringBuilder();

            foreach (var bucket in buckets)
            {
                extrasInstaller.AppendLine($"scoop bucket add {bucket}");
            }

            return extrasInstaller.ToString();
        }

        public string Build(params string[] buckets)
        {
            return Build(buckets.ToList());
        }
    }
}
