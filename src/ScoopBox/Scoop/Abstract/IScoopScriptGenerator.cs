using System.Threading.Tasks;

namespace ScoopBox.Scoop.Abstract
{
    public interface IScoopScriptGenerator
    {
        Task Generate(ScoopBoxOptions scoopBoxOptions);
    }
}
