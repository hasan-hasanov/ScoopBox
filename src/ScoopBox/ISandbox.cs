using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run();

        Task Run(IEnumerable<string> applications);

        Task Run(FileStream scriptBefore, IEnumerable<string> applications);

        Task Run(FileStream scriptBefore, IEnumerable<string> applications, FileStream scriptAfter);
    }
}
