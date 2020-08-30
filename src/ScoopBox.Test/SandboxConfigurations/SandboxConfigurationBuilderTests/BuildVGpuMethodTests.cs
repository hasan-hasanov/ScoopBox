using ScoopBox.Entities;
using ScoopBox.Enums;
using ScoopBox.SandboxConfigurations;
using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.SandboxConfigurationBuilderTests
{
    public class BuildVGpuMethodTests
    {
        private readonly XmlSerializer _configurationSerializer;
        private readonly XmlSerializerNamespaces _emptyNamespaces;
        private readonly XmlWriterSettings _configurationSettings;

        public BuildVGpuMethodTests()
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
        public async Task ShouldBuildConfigurationFileWithVGpu()
        {
            string location = "C:/temp/";
            string configurationName = "sandbox.wsb";

            Configuration expectedRaw = new Configuration() { VGpu = Enum.GetName(typeof(VGpuOptions), VGpuOptions.Enabled) };
            using var configStringWriter = new StringWriter();
            using XmlWriter configXmlWriter = XmlWriter.Create(configStringWriter, _configurationSettings);

            IOptions options = new Options() { RootFilesDirectoryLocation = location, SandboxConfigurationFileName = configurationName, VGpu = VGpuOptions.Enabled };
            MockFileSystem mockFileSystem = new MockFileSystem();
            _configurationSerializer.Serialize(configXmlWriter, expectedRaw, _emptyNamespaces);

            string expected = configStringWriter.ToString();

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options, mockFileSystem);
            sandboxConfigurationBuilder.BuildVGpu();
            await sandboxConfigurationBuilder.CreatePartialConfigurationFile();

            MockFileData mockFile = mockFileSystem.GetFile(Path.Combine(location, configurationName));
            string actual = mockFile.TextContents;

            Assert.True(expected == actual);
        }
    }
}
