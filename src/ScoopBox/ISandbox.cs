using ScoopBox.Scripts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run(IScript script, CancellationToken cancellationToken = default);

        Task Run(List<IScript> scripts, CancellationToken cancellationToken = default);
    }
}
