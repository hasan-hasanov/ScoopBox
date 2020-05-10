using System.Collections.Generic;

namespace ScoopBox.Scripts.InstallerScripts.Abstract
{
    public interface IAppInstallerBuilder
    {
        string Build(List<string> apps);

        string Build(string app);
    }
}
