using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run(string literalScript);

        Task Run(List<string> literalScripts);
    }
}
