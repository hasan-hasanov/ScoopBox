using System.Collections.Generic;

namespace ScoopBox.Scripts.InstallerScripts.Abstract
{
    public interface IScoopBucketsBuilder
    {
        string Build(List<string> buckets);

        string Build(params string[] buckets);
    }
}
