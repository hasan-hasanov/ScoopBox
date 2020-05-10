using System.Collections.Generic;
using System.Xml.Serialization;

namespace ScoopBox.Entities
{
    [XmlRoot(ElementName = nameof(LogonCommand))]
    public class LogonCommand
    {
        [XmlElement(ElementName = nameof(Command))]
        public List<string> Command { get; set; }
    }
}
