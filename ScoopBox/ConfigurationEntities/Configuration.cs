using System.Xml.Serialization;

namespace ScoopBox.ConfigurationEntities
{
    [XmlRoot(ElementName = nameof(Configuration))]
    public class Configuration
    {
        [XmlElement(ElementName = nameof(VGpu))]
        public string VGpu { get; set; }

        [XmlElement(ElementName = nameof(Networking))]
        public string Networking { get; set; }

        [XmlElement(ElementName = nameof(LogonCommand))]
        public LogonCommand LogonCommand { get; set; }
    }
}
