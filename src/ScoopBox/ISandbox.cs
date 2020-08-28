using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run();

        Task Run(FileStream script);

        Task Run(IEnumerable<FileStream> scripts);

        Task Run(IEnumerable<string> applications);

        Task Run(FileStream scriptBefore, IEnumerable<string> applications);

        Task Run(IEnumerable<FileStream> scriptsBefore, IEnumerable<string> applications);

        Task Run(FileStream scriptBefore, IEnumerable<string> applications, FileStream scriptsAfter);

        Task Run(IEnumerable<FileStream> scriptsBefore, IEnumerable<string> applications, FileStream scriptAfter);

        Task Run(FileStream scriptBefore, IEnumerable<string> applications, IEnumerable<FileStream> scriptsAfter);

        Task Run(IEnumerable<FileStream> scriptsBefore, IEnumerable<string> applications, IEnumerable<FileStream> scriptsAfter);
    }
}
