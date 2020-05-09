using System.Collections.Generic;

namespace ScoopBox.Scripts.Abstract
{
    public interface IAppInstallerBuilder
    {
        string Build(List<string> apps);
    }
}
