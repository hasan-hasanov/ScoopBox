using ScoopBox.Abstractions;
using ScoopBox.Entities;
using ScoopBox.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("ScoopBox.Test")]
namespace ScoopBox.SandboxConfigurations
{
    /// <summary>
    /// Provides an implementation of <see cref="ISandboxConfigurationBuilder"/> to build a configuration file
    /// </summary>
    public class SandboxConfigurationBuilder : ISandboxConfigurationBuilder
    {
        private readonly IOptions _options;
        private readonly Configuration _configuration;

        private readonly XmlSerializer _configurationSerializer;
        private readonly XmlSerializerNamespaces _emptyNamespaces;
        private readonly XmlWriterSettings _configurationSettings;

        private readonly Func<string, byte[], CancellationToken, Task> _writeAllBytesAsync;

        /// <summary>Initializes a new instance of the <see cref="SandboxConfigurationBuilder"/> class.</summary>
        /// <param name="options">
        /// Enables the user to control some aspects of Windows Sandbox.
        /// </param>
        public SandboxConfigurationBuilder(IOptions options)
            : this(
                 options,
                 new Configuration(),
                 new XmlSerializer(typeof(Configuration)),
                 new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }),
                 new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true },
                 FileSystemAbstractions.WriteAllBytesAsync)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SandboxConfigurationBuilder"/> class.
        /// This constructor is solely for testing purposes and contains framework specific classes that cannot be tested.
        /// </summary>
        /// <param name="options">
        /// Enables the user to control some aspects of Windows Sandbox.
        /// </param>
        /// <param name="configuration">
        /// User provided configurations to generate sandbox configuration file.
        /// </param>
        /// <param name="configurationSerializer">
        /// Default serializer for the xml document.
        /// </param>
        /// <param name="emptyNamespaces">
        /// Removes namespaced defined by xml specification.
        /// </param>
        /// <param name="configurationSettings">
        /// Specify indentation and omits xml declaration.
        /// </param>
        /// <param name="writeAllBytesAsync">
        /// Delegate that takes path, content and cancellation token and writes to a file.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        internal SandboxConfigurationBuilder(
            IOptions options,
            Configuration configuration,
            XmlSerializer configurationSerializer,
            XmlSerializerNamespaces emptyNamespaces,
            XmlWriterSettings configurationSettings,
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configurationSerializer == null)
            {
                throw new ArgumentNullException(nameof(configurationSerializer));
            }

            if (emptyNamespaces == null)
            {
                throw new ArgumentNullException(nameof(emptyNamespaces));
            }

            if (configurationSettings == null)
            {
                throw new ArgumentNullException(nameof(configurationSettings));
            }

            if (writeAllBytesAsync == null)
            {
                throw new ArgumentNullException(nameof(writeAllBytesAsync));
            }

            _options = options;
            _configuration = configuration;

            _configurationSerializer = configurationSerializer;
            _emptyNamespaces = emptyNamespaces;
            _configurationSettings = configurationSettings;

            _writeAllBytesAsync = writeAllBytesAsync;
        }

        /// <summary>
        /// Builds windows conifguration file in location provided with <see cref="IOptions.RootFilesDirectoryLocation"/>.
        /// </summary>
        /// <param name="logonCommand">
        /// The command that will be executed when the Windows Sandbox boots up.
        /// By default this command is a powershell command that executes MainScript.ps1 file which inside contains all the user defined commands.
        /// This is a current limitation of Windows Sandbox Configuration.
        /// Although it takes a collection of elements, currently only one command can be executed in the sandbox.
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

                await _writeAllBytesAsync(path, content, cancellationToken);
            }
        }
    }
}
