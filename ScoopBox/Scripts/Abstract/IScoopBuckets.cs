using System.Collections.Generic;

namespace ScoopBox.Scripts.Abstract
{
    public interface IScoopBuckets
    {
        string Add(List<string> buckets);

        string Add(params string[] buckets);
    }
}
