using ScoopBox.Scripts.Abstract;
using System.Collections.Generic;
using System.Text;

namespace ScoopBox.Scripts
{
    public class AppInstallerBuilder : IAppInstallerBuilder
    {
        public string Build(List<string> apps)
        {
            StringBuilder appsBuilder = new StringBuilder();

            appsBuilder.Append("scoop install ");
            foreach (var app in apps)
            {
                appsBuilder.Append($"{app} ");
            }

            appsBuilder.AppendLine();
            return appsBuilder.ToString();
        }
    }
}
