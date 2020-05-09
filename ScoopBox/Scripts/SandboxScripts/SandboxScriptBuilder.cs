using ScoopBox.ConfigurationEntities;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ScoopBox.Scripts.SandboxScripts
{
    public class SandboxScriptBuilder : ISandboxScriptBuilder
    {
        public string Build(Configuration configuration)
        {
            XmlSerializer configurationSerializer = new XmlSerializer(typeof(Configuration));
            XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            using (var configStringWriter = new StringWriter())
            {
                using (XmlWriter configXmlWriter = XmlWriter.Create(configStringWriter, settings))
                {
                    configurationSerializer.Serialize(configXmlWriter, configuration, emptyNamespaces);
                    return configStringWriter.ToString();
                }
            }
        }
    }
}
