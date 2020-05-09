using ScoopBox.Enums;
using System;
using System.Xml.Serialization;

namespace ScoopBox.ConfigurationEntities
{
    [XmlRoot(ElementName = nameof(Configuration))]
    public class Configuration
    {
        public Configuration()
        {
        }

        public Configuration(ScoopBoxOptions options)
        {
            this.LogonCommand = new LogonCommand();

            this.VGpu = Enum.GetName(typeof(VGpuOptions), options.VGpu);
            this.LogonCommand.Command = $@"{options.SandboxFilesPath}\sandbox.ps1";
        }

        [XmlElement(ElementName = nameof(VGpu))]
        public string VGpu { get; set; }

        [XmlElement(ElementName = nameof(Networking))]
        public string Networking => "true";

        [XmlElement(ElementName = nameof(LogonCommand))]
        public LogonCommand LogonCommand { get; set; }
    }
}
