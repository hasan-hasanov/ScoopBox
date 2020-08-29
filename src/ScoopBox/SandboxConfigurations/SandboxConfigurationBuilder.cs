using ScoopBox.Entities;
using ScoopBox.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public IList<string> Commands { get; }

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

            Commands = new List<string>();
        }

        public void AddCommand(string command)
        {
            Commands.Add(command);
        }

        public void AddCommands(IEnumerable<string> commands)
        {
            foreach (var command in commands)
            {
                Commands.Add(command);
            }
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
            if (Commands != null && Commands.Any())
            {
                _configuration.LogonCommand = new LogonCommand
                {
                    Command = Commands.ToList()
                };
            }
        }

        public async Task CreatePartialConfigurationFile()
        {
            string content = SerializeXMLToString();
            using (StreamWriter writer = File.CreateText(Path.Combine(_options.RootFilesDirectoryLocation, _options.SandboxConfigurationFileName)))
            {
                await writer.WriteAsync(content);
            }
        }

        public async Task CreateConfigurationFile()
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

            await CreatePartialConfigurationFile();
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
