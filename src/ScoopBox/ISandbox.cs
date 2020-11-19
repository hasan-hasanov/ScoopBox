using ScoopBox.PackageManager;
using ScoopBox.Scripts;
using ScoopBox.Translators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run(string literalScript);

        Task Run(List<string> literalScripts);

        Task Run(IPackageManager packageManager);

        Task Run(IScript script, IPowershellTranslator translator);

        Task Run(List<Tuple<IScript, IPowershellTranslator>> scripts);
    }
}
