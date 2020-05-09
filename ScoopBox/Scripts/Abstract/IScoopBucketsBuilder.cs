using System.Collections.Generic;

namespace ScoopBox.Scripts.Abstract
{
    public interface IScoopBucketsBuilder
    {
        string Build(List<string> buckets);

        string Build(params string[] buckets);
    }
}
