using ScoopBox.Scripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts
{
    public class ScoopInstaller : IScoopInstaller
    {
        public string Install()
        {
            StringBuilder installerBuilder = new StringBuilder();

            installerBuilder.Append("Invoke-Expression");
            installerBuilder.Append(" (New-Object");
            installerBuilder.Append(" System.Net.WebClient)");
            installerBuilder.Append(".DownloadString");
            installerBuilder.Append("('https://get.scoop.sh')");

            return installerBuilder.ToString();
        }
    }
}
