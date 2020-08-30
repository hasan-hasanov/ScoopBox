using ScoopBox.Entities;
using ScoopBox.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
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
        private readonly IFileSystem _fileSystem;

        private readonly XmlSerializer _configurationSerializer;
        private readonly XmlSerializerNamespaces _emptyNamespaces;
        private readonly XmlWriterSettings _configurationSettings;

        public IList<string> Commands { get; }

        public SandboxConfigurationBuilder(IOptions options)
            : this(options, new FileSystem())
        {
        }

        public SandboxConfigurationBuilder(IOptions options, IFileSystem fileSystem)
        {
            _configuration = new Configuration();
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

            _configurationSerializer = new XmlSerializer(typeof(Configuration));
            _emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            _configurationSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            Commands = new List<string>();
        }

        public virtual void AddCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentNullException(nameof(command));
            }

            Commands.Add(command);
        }

        public virtual void AddCommands(IEnumerable<string> commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            if (!commands.Any())
            {
                throw new ArgumentException($"Parameter {nameof(commands)} is empty!");
            }

            foreach (var command in commands)
            {
                Commands.Add(command);
            }
        }

        public virtual void BuildVGpu()
        {
            _configuration.VGpu = Enum.GetName(typeof(VGpuOptions), _options.VGpu);
        }

        public virtual void BuildNetworking()
        {
            _configuration.Networking = Enum.GetName(typeof(NetworkingOptions), _options.Networking);
        }

        public virtual void BuildAudioInput()
        {
            _configuration.AudioInput = Enum.GetName(typeof(AudioInputOptions), _options.AudioInput);
        }

        public virtual void BuildVideoInput()
        {
            _configuration.VideoInput = Enum.GetName(typeof(VideoInputOptions), _options.VideoInput);
        }

        public virtual void BuildProtectedClient()
        {
            _configuration.ProtectedClient = Enum.GetName(typeof(ProtectedClientOptions), _options.ProtectedClient);
        }

        public virtual void BuildPrinterRedirection()
        {
            _configuration.PrinterRedirection = Enum.GetName(typeof(PrinterRedirectionOptions), _options.PrinterRedirection);
        }

        public virtual void BuildClipboardRedirection()
        {
            _configuration.ClipboardRedirection = Enum.GetName(typeof(ClipboardRedirectionOptions), _options.ClipboardRedirection);
        }

        public virtual void BuildMemoryInMB()
        {
            _configuration.MemoryInMB = _options.MemoryInMB.ToString();
        }

        public virtual void BuildMappedFolders()
        {
            _configuration.MappedFolders = new MappedFolders()
            {
                MappedFolder = new List<MappedFolder>()
            };

            _configuration.MappedFolders.MappedFolder.AddRange(_options.UserMappedDirectories);

            _configuration.MappedFolders.MappedFolder.Add(new MappedFolder()
            {
                HostFolder = _options.RootFilesDirectoryLocation,
                SandboxFolder = _options.RootSandboxFilesDirectoryLocation,
                ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.False).ToLower()
            });
        }

        public virtual void BuildLogonCommand()
        {
            if (Commands != null && Commands.Any())
            {
                _configuration.LogonCommand = new LogonCommand
                {
                    Command = Commands.ToList()
                };
            }
        }

        public virtual async Task CreatePartialConfigurationFile()
        {
            string content = SerializeXMLToString();
            using (StreamWriter writer = _fileSystem.File.CreateText(Path.Combine(_options.RootFilesDirectoryLocation, _options.SandboxConfigurationFileName)))
            {
                await writer.WriteAsync(content);
            }
        }

        public virtual async Task CreateConfigurationFile()
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
