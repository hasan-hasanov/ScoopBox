using ScoopBox.ConfigurationEntities;

namespace ScoopBox.Scripts.SandboxScripts.Abstract
{
    public interface IConfigurationBuilder
    {
        Configuration Build(ScoopBoxOptions options);
    }
}
