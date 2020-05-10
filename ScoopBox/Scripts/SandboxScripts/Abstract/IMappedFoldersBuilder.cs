using ScoopBox.Entities;

namespace ScoopBox.Scripts.SandboxScripts.Abstract
{
    public interface IMappedFoldersBuilder
    {
        MappedFolders Build(ScoopBoxOptions options);
    }
}
