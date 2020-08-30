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
    public class CreatePartialConfigurationFileMethodTests
    {
        private readonly XmlSerializer _configurationSerializer;
        private readonly XmlSerializerNamespaces _emptyNamespaces;
        private readonly XmlWriterSettings _configurationSettings;

        public CreatePartialConfigurationFileMethodTests()
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
        public async Task ShouldBuildConfigurationFilePartialData()
        {
            string localDirectoryLocation = "C:/temp/";
            string sandboxDirectoryLocation = "C:/Sandbox/temp";
            string configurationName = "sandbox.wsb";

            string firstCommand = "powershell.exe C:/temp/script1.ps1";
            string secondCommand = "powershell.exe C:/temp/script2.ps1";

            Configuration expectedRaw = new Configuration()
            {
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
            sandboxConfigurationBuilder.BuildLogonCommand();
            sandboxConfigurationBuilder.BuildMappedFolders();
            await sandboxConfigurationBuilder.CreatePartialConfigurationFile();

            MockFileData mockFile = mockFileSystem.GetFile(Path.Combine(localDirectoryLocation, configurationName));
            string actual = mockFile.TextContents;

            Assert.True(expected == actual);
        }
    }
}
