using System.Threading.Tasks;

namespace ScoopBox.Abstract
{
    public interface ISandboxGenerator
    {
        Task Generate(ScoopBoxOptions options);
    }
}
