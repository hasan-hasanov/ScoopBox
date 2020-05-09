using ScoopBox.Scripts.Abstract;
using System.Collections.Generic;
using System.Text;

namespace ScoopBox.Scripts
{
    public class AppInstaller : IAppInstaller
    {
        public string Install(List<string> apps)
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
