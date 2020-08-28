using ScoopBox.Entities;
using ScoopBox.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ScoopBox
{
    public class SandboxBuilder : ISandboxScriptBuilder
    {
        private readonly Configuration _configuration;
        private readonly Options _options;

        private readonly XmlSerializer _configurationSerializer;
        private readonly XmlSerializerNamespaces _emptyNamespaces;
        private readonly XmlWriterSettings _configurationSettings;

        public SandboxBuilder(Options options)
        {
            _configuration = new Configuration();
            _options = options;

            _configurationSerializer = new XmlSerializer(typeof(Configuration));
            _emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            _configurationSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
        }

        public void BuildVGpu()
        {
            _configuration.VGpu = Enum.GetName(typeof(VGpuOptions), _options.VGpu);
        }

        public void BuildNetworking()
        {
            _configuration.Networking = Enum.GetName(typeof(VGpuOptions), _options.Networking);
        }

        public void BuildMappedFolders()
        {
            _configuration.MappedFolders = new MappedFolders()
            {
                MappedFolder = new List<MappedFolder>()
            };

            foreach (var directoryConfig in _options.UserMappedDirectories)
            {
                _configuration.MappedFolders.MappedFolder.Add(new MappedFolder()
                {
                    HostFolder = directoryConfig.Key,
                    ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), directoryConfig.Value).ToLower()
                });
            }

            _configuration.MappedFolders.MappedFolder.Add(new MappedFolder()
            {
                // TODO: Make these come from the options
                HostFolder = Directory.CreateDirectory($"{Path.GetTempPath()}/{Constants.SandboxFolderName}").FullName,
                ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.False).ToLower()
            });
        }

        public void BuildLogonCommand()
        {
            StringBuilder logonBuilder = new StringBuilder();

            logonBuilder
                .Append("powershell.exe ")
                .Append("-ExecutionPolicy ")
                .Append("Bypass ")
                .Append("-File ")
                .Append(Constants.SandboxInstallerLocation);

            logonBuilder.AppendLine();

            logonBuilder
                .Append("powershell.exe ")
                .Append($"\"{Constants.SandboxInstallerLocation}\"");

            logonBuilder.AppendLine();
        }

        public string Build()
        {
            this.BuildVGpu();
            this.BuildNetworking();
            this.BuildMappedFolders();
            this.BuildLogonCommand();

            using (var configStringWriter = new StringWriter())
            {
                using (XmlWriter configXmlWriter = XmlWriter.Create(configStringWriter, _configurationSettings))
                {
                    _configurationSerializer.Serialize(configXmlWriter, _configuration, _emptyNamespaces);
                    return configStringWriter.ToString();
                }
            }
        }
    }
}
