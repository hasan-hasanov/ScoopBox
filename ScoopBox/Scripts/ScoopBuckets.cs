using ScoopBox.Scripts.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoopBox.Scripts
{
    public class ScoopBuckets : IScoopBuckets
    {
        public string Add(List<string> buckets)
        {
            StringBuilder extrasInstaller = new StringBuilder();

            foreach (var bucket in buckets)
            {
                extrasInstaller.AppendLine($"scoop bucket add {bucket}");
            }

            return extrasInstaller.ToString();
        }

        public string Add(params string[] buckets)
        {
            return this.Add(buckets.ToList());
        }
    }
}
