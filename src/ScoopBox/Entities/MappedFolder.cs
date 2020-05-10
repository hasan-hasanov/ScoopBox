using System.Xml.Serialization;

namespace ScoopBox.Entities
{
    [XmlRoot(ElementName = nameof(MappedFolder))]
    public class MappedFolder
    {
        [XmlElement(ElementName = nameof(HostFolder))]
        public string HostFolder { get; set; }

        [XmlElement(ElementName = nameof(ReadOnly))]
        public string ReadOnly { get; set; }
    }
}
