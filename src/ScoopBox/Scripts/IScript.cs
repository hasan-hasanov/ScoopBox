using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox.Scripts
{
    public interface IScript
    {
        FileSystemInfo ScriptFile { get; set; }

        Task CopyOrMaterialize(IOptions options, CancellationToken cancellationToken = default);
    }
}
