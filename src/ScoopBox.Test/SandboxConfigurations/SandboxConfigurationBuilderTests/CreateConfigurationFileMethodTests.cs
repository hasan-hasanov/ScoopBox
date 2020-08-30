using ScoopBox.Entities;
using ScoopBox.Enums;
using ScoopBox.SandboxConfigurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.SandboxConfigurationBuilderTests
{
    public class CreateConfigurationFileMethodTests
    {
        private readonly XmlSerializer _configurationSerializer;
        private readonly XmlSerializerNamespaces _emptyNamespaces;
        private readonly XmlWriterSettings _configurationSettings;

        public CreateConfigurationFileMethodTests()
        {
            _configurationSerializer = new XmlSerializer(typeof(Configuration));
            _emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            _configurationSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
        }

        [Fact]
        public async Task ShouldBuildCompleteConfigurationFile()
        {
            string localDirectoryLocation = "C:/temp/";
            string sandboxDirectoryLocation = "C:/Sandbox/temp";
            string configurationName = "sandbox.wsb";

            string firstCommand = "powershell.exe C:/temp/script1.ps1";
            string secondCommand = "powershell.exe C:/temp/script2.ps1";

            Configuration expectedRaw = new Configuration()
            {
                AudioInput = Enum.GetName(typeof(AudioInputOptions), AudioInputOptions.Enable),
                ClipboardRedirection = Enum.GetName(typeof(ClipboardRedirectionOptions), ClipboardRedirectionOptions.Disable),
                MemoryInMB = "500",
                Networking = Enum.GetName(typeof(NetworkingOptions), NetworkingOptions.Disable),
                PrinterRedirection = Enum.GetName(typeof(PrinterRedirectionOptions), PrinterRedirectionOptions.Enable),
                ProtectedClient = Enum.GetName(typeof(ProtectedClientOptions), ProtectedClientOptions.Enable),
                VGpu = Enum.GetName(typeof(VGpuOptions), VGpuOptions.Disabled),
                VideoInput = Enum.GetName(typeof(VideoInputOptions), VideoInputOptions.Enable),
                LogonCommand = new LogonCommand()
                {
                    Command = new List<string>()
                    {
                        firstCommand,
                        secondCommand
                    }
                },
                MappedFolders = new MappedFolders()
                {
                    MappedFolder = new List<MappedFolder>()
                    {
                        new MappedFolder()
                        {
                            HostFolder = "C:/temp1",
                            SandboxFolder = "C:/Sandbox/temp1",
                            ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.True).ToLower()
                        },
                        new MappedFolder()
                        {
                            HostFolder = "C:/temp2",
                            SandboxFolder = "C:/Sandbox/temp2",
                            ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.True).ToLower()
                        },
                        new MappedFolder()
                        {
                            HostFolder = localDirectoryLocation,
                            SandboxFolder = sandboxDirectoryLocation,
                            ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.False).ToLower()
                        }
                    }
                }
            };

            using var configStringWriter = new StringWriter();
            using XmlWriter configXmlWriter = XmlWriter.Create(configStringWriter, _configurationSettings);

            IOptions options = new Options()
            {
                RootFilesDirectoryLocation = localDirectoryLocation,
                RootSandboxFilesDirectoryLocation = sandboxDirectoryLocation,
                SandboxConfigurationFileName = configurationName,
                AudioInput = AudioInputOptions.Enable,
                ClipboardRedirection = ClipboardRedirectionOptions.Disable,
                MemoryInMB = 500,
                Networking = NetworkingOptions.Disable,
                PrinterRedirection = PrinterRedirectionOptions.Enable,
                ProtectedClient = ProtectedClientOptions.Enable,
                VGpu = VGpuOptions.Disabled,
                VideoInput = VideoInputOptions.Enable,
                UserMappedDirectories = new List<MappedFolder>()
                {
                    new MappedFolder()
                    {
                        HostFolder = "C:/temp1",
                        SandboxFolder = "C:/Sandbox/temp1",
                        ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.True).ToLower()
                    },
                    new MappedFolder()
                    {
                        HostFolder = "C:/temp2",
                        SandboxFolder = "C:/Sandbox/temp2",
                        ReadOnly = Enum.GetName(typeof(ReadOnlyOptions), ReadOnlyOptions.True).ToLower()
                    }
                }
            };

            MockFileSystem mockFileSystem = new MockFileSystem();
            _configurationSerializer.Serialize(configXmlWriter, expectedRaw, _emptyNamespaces);

            string expected = configStringWriter.ToString();

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options, mockFileSystem);
            sandboxConfigurationBuilder.AddCommand(firstCommand);
            sandboxConfigurationBuilder.AddCommand(secondCommand);
            await sandboxConfigurationBuilder.CreateConfigurationFile();

            MockFileData mockFile = mockFileSystem.GetFile(Path.Combine(localDirectoryLocation, configurationName));
            string actual = mockFile.TextContents;

            Assert.True(expected == actual);
        }
    }
}
