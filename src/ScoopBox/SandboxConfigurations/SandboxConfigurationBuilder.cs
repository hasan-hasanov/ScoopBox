using ScoopBox.Entities;
using ScoopBox.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
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

        public SandboxConfigurationBuilder(IOptions options)
            : this(options, new FileSystem())
        {
        }

        private SandboxConfigurationBuilder(IOptions options, IFileSystem fileSystem)
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
        }

        public virtual async Task Build(string logonCommand, CancellationToken cancellationToken = default)
        {
            _configuration.VGpu = Enum.GetName(typeof(VGpuOptions), _options.VGpu);
            _configuration.Networking = Enum.GetName(typeof(NetworkingOptions), _options.Networking);
            _configuration.AudioInput = Enum.GetName(typeof(AudioInputOptions), _options.AudioInput);
            _configuration.VideoInput = Enum.GetName(typeof(VideoInputOptions), _options.VideoInput);
            _configuration.ProtectedClient = Enum.GetName(typeof(ProtectedClientOptions), _options.ProtectedClient);
            _configuration.PrinterRedirection = Enum.GetName(typeof(PrinterRedirectionOptions), _options.PrinterRedirection);
            _configuration.ClipboardRedirection = Enum.GetName(typeof(ClipboardRedirectionOptions), _options.ClipboardRedirection);
            _configuration.MemoryInMB = _options.MemoryInMB.ToString();

            _configuration.MappedFolders = new MappedFolders()
            {
                MappedFolder = new List<MappedFolder>(_options.UserMappedDirectories)
            };

            _configuration.MappedFolders.MappedFolder.Add(new MappedFolder()
            {
                HostFolder = _options.RootFilesDirectoryLocation,
                SandboxFolder = _options.RootSandboxFilesDirectoryLocation,
                ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.False).ToLower()
            });

            _configuration.LogonCommand = new LogonCommand
            {
                Command = new List<string>() { logonCommand }
            };

            await GenerateFile(cancellationToken);
        }

        private async Task GenerateFile(CancellationToken cancellationToken)
        {
            using (StringWriter configStringWriter = new StringWriter())
            using (XmlWriter configXmlWriter = XmlWriter.Create(configStringWriter, _configurationSettings))
            {
                _configurationSerializer.Serialize(configXmlWriter, _configuration, _emptyNamespaces);

                byte[] content = new UTF8Encoding().GetBytes(configStringWriter.ToString());
                string path = Path.Combine(_options.RootFilesDirectoryLocation, _options.SandboxConfigurationFileName);

                await File.WriteAllBytesAsync(path, content, cancellationToken);
            }
        }
    }
}
