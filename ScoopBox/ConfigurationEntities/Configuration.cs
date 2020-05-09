using System.Xml.Serialization;

namespace ScoopBox.ConfigurationEntities
{
    [XmlRoot(ElementName = "Configuration")]
    public class Configuration
    {
        [XmlElement(ElementName = "VGpu")]
        public string VGpu { get; set; }

        [XmlElement(ElementName = "Networking")]
        public string Networking { get; set; }

        [XmlElement(ElementName = "LogonCommand")]
        public LogonCommand LogonCommand { get; set; }
    }
}
