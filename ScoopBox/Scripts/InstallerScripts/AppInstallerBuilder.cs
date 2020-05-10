using ScoopBox.Scripts.InstallerScripts.Abstract;
using System.Collections.Generic;
using System.Text;

namespace ScoopBox.Scripts.InstallerScripts
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

        public string Build(string app)
        {
            StringBuilder appsBuilder = new StringBuilder();
            appsBuilder.AppendLine($"scoop install {app}");

            return appsBuilder.ToString();
        }
    }
}
