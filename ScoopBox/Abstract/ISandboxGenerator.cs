using System.Threading.Tasks;

namespace ScoopBox.Abstract
{
    public interface ISandboxGenerator
    {
        Task Generate(SandboxOptions options);
    }
}
