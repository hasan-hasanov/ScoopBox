using ScoopBox.PackageManager;
using ScoopBox.Scripts;
using ScoopBox.Translators;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run(string literalScript, CancellationToken cancellationToken = default);

        Task Run(List<string> literalScripts, CancellationToken cancellationToken = default);

        Task Run(IPackageManager packageManager, CancellationToken cancellationToken = default);

        Task Run(IScript script, IPowershellTranslator translator, CancellationToken cancellationToken = default);

        Task Run(List<Tuple<IScript, IPowershellTranslator>> scripts, CancellationToken cancellationToken = default);
    }
}
