using ScoopBox.CommandTranslators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run(string literalScript);

        Task Run(FileSystemInfo script, ICommandTranslator commandTranslator);

        Task Run(List<string> literalScripts);

        Task Run(List<Tuple<FileSystemInfo, ICommandTranslator>> scripts);
    }
}
