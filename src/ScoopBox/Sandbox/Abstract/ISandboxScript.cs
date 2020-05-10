using System.Threading.Tasks;

namespace ScoopBox.Sandbox.Abstract
{
    public interface ISandboxScript
    {
        Task Generate(ScoopBoxOptions options);
    }
}
