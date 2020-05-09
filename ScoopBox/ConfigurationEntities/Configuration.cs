using ScoopBox.Enums;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ScoopBox.ConfigurationEntities
{
    [XmlRoot(ElementName = nameof(Configuration))]
    public class Configuration
    {
        public Configuration()
        {
        }

        public Configuration(
            ScoopBoxOptions options,
            List<string> commands)
        {
            this.LogonCommand = new LogonCommand();

            this.VGpu = Enum.GetName(typeof(VGpuOptions), options.VGpu);
            this.LogonCommand.Command = commands;
        }

        [XmlElement(ElementName = nameof(VGpu))]
        public string VGpu { get; set; }

        [XmlElement(ElementName = nameof(Networking))]
        public string Networking => "true";

        [XmlElement(ElementName = nameof(LogonCommand))]
        public LogonCommand LogonCommand { get; set; }
    }
}
