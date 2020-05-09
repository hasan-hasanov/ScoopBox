using System.Collections.Generic;

namespace ScoopBox.Scripts.Abstract
{
    public interface IAppInstaller
    {
        string Install(List<string> apps);
    }
}
