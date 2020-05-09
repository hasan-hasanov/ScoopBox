using System.Threading.Tasks;

namespace ScoopBox.Sandbox.Abstract
{
    public interface ISandboxScriptGenerator
    {
        Task Generate(ScoopBoxOptions options);
    }
}
