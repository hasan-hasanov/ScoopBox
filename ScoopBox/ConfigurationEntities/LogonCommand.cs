using System.Xml.Serialization;

namespace ScoopBox.ConfigurationEntities
{
    [XmlRoot(ElementName = nameof(LogonCommand))]
    public class LogonCommand
    {
        [XmlElement(ElementName = nameof(Command))]
        public string Command { get; set; }
    }
}
