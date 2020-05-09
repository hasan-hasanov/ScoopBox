using System.Threading.Tasks;

namespace ScoopBox.Abstract
{
    public interface IScriptGenerator
    {
        Task Generate(ScoopBoxOptions scoopBoxOptions);
    }
}
