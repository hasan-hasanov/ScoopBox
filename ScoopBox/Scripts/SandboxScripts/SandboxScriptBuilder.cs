using ScoopBox.Entities;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ScoopBox.Scripts.SandboxScripts
{
    public class SandboxScriptBuilder : ISandboxScriptBuilder
    {
        private readonly IConfigurationBuilder configurationBuilder;

        public SandboxScriptBuilder(IConfigurationBuilder configurationBuilder)
        {
            this.configurationBuilder = configurationBuilder;
        }

        public string Build(ScoopBoxOptions options)
        {
            Configuration configuration = this.configurationBuilder.Build(options);

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
