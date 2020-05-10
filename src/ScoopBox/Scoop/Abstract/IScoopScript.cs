using System.Threading.Tasks;

namespace ScoopBox.Scoop.Abstract
{
    public interface IScoopScript
    {
        Task Generate(ScoopBoxOptions scoopBoxOptions);
    }
}
