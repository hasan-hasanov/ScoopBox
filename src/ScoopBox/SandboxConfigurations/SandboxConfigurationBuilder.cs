using ScoopBox.Entities;
using ScoopBox.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ScoopBox.SandboxConfigurations
{
    public class SandboxConfigurationBuilder : ISandboxConfigurationBuilder
    {
        private readonly Configuration _configuration;
        private readonly IOptions _options;

        private readonly XmlSerializer _configurationSerializer;
        private readonly XmlSerializerNamespaces _emptyNamespaces;
        private readonly XmlWriterSettings _configurationSettings;

        public SandboxConfigurationBuilder(IOptions options)
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
            _configuration.Networking = Enum.GetName(typeof(NetworkingOptions), _options.Networking);
        }

        public void BuildAudioInput()
        {
            _configuration.AudioInput = Enum.GetName(typeof(AudioInputOptions), _options.AudioInput);
        }

        public void BuildVideoInput()
        {
            _configuration.VideoInput = Enum.GetName(typeof(VideoInputOptions), _options.VideoInput);
        }

        public void BuildProtectedClient()
        {
            _configuration.ProtectedClient = Enum.GetName(typeof(ProtectedClientOptions), _options.ProtectedClient);
        }

        public void BuildPrinterRedirection()
        {
            _configuration.PrinterRedirection = Enum.GetName(typeof(PrinterRedirectionOptions), _options.PrinterRedirection);
        }

        public void BuildClipboardRedirection()
        {
            _configuration.ClipboardRedirection = Enum.GetName(typeof(ClipboardRedirectionOptions), _options.ClipboardRedirection);
        }

        public void BuildMemoryInMB()
        {
            _configuration.MemoryInMB = _options.MemoryInMB.ToString();
        }

        public void BuildMappedFolders()
        {
            _configuration.MappedFolders = new MappedFolders()
            {
                MappedFolder = new List<MappedFolder>()
            };

            _configuration.MappedFolders.MappedFolder.AddRange(_options.UserMappedDirectories);

            // TODO: Consider using mapped folder here too.
            _configuration.MappedFolders.MappedFolder.Add(new MappedFolder()
            {
                HostFolder = _options.RootFilesDirectoryLocation,
                SandboxFolder = _options.RootSandboxFilesDirectoryLocation,
                ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.False).ToLower()
            });
        }

        public void BuildLogonCommand()
        {
            // TODO: Decide for one or multiple package managers!!!

            //StringBuilder logonBuilder = new StringBuilder();

            //logonBuilder
            //    .Append("powershell.exe ")
            //    .Append("-ExecutionPolicy ")
            //    .Append("Bypass ")
            //    .Append("-File ")
            //    .Append(Constants.SandboxInstallerLocation);

            //logonBuilder.AppendLine();

            //logonBuilder
            //    .Append("powershell.exe ")
            //    .Append($"\"{Constants.SandboxInstallerLocation}\"");

            //logonBuilder.AppendLine();
        }

        public string BuildPartial()
        {
            return SerializeXMLToString();
        }

        public string Build()
        {
            BuildVGpu();
            BuildNetworking();
            BuildAudioInput();
            BuildVideoInput();
            BuildPrinterRedirection();
            BuildClipboardRedirection();
            BuildProtectedClient();
            BuildMemoryInMB();
            BuildMappedFolders();
            BuildLogonCommand();

            return SerializeXMLToString();
        }

        private string SerializeXMLToString()
        {
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
