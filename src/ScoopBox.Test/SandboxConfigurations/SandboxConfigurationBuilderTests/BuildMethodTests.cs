using ScoopBox.Entities;
using ScoopBox.SandboxConfigurations;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.SandboxConfigurationBuilderTests
{
    public class BuildMethodTests
    {
        [Fact]
        public async Task ShouldGenerateCorrectConfigurationContent()
        {
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            string builderContent = string.Empty;
            string mockLogonCommand = "Mock logon command";

            IOptions options = new Options();
            Configuration configuration = new Configuration();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (path, content, token) =>
            {
                builderContent = uTF8Encoding.GetString(content);
                return Task.CompletedTask;
            };

            ISandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(
                options,
                configuration,
                xmlSerializer,
                xmlSerializerNamespaces,
                xmlWriterSettings,
                writeAllBytesAsync);

            await sandboxConfigurationBuilder.Build(mockLogonCommand);

            string actual = builderContent;
            string expected = @"<Configuration>
  <VGpu>Disabled</VGpu>
  <Networking>Default</Networking>
  <MappedFolders>
    <MappedFolder>
      <HostFolder>C:\Users\Hasan Hasanov\AppData\Local\Temp\Sandbox</HostFolder>
      <SandboxFolder>C:\Users\WDAGUtilityAccount\Desktop\Sandbox\</SandboxFolder>
      <ReadOnly>false</ReadOnly>
    </MappedFolder>
  </MappedFolders>
  <LogonCommand>
    <Command>Mock logon command</Command>
  </LogonCommand>
  <AudioInput>Default</AudioInput>
  <VideoInput>Default</VideoInput>
  <ProtectedClient>Default</ProtectedClient>
  <PrinterRedirection>Default</PrinterRedirection>
  <ClipboardRedirection>Default</ClipboardRedirection>
  <MemoryInMB>0</MemoryInMB>
</Configuration>";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldGenerateCorrectConfigurationPath()
        {
            string mockLogonCommand = "Mock logon command";
            string filePath = string.Empty;

            IOptions options = new Options();
            Configuration configuration = new Configuration();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = (path, content, token) =>
            {
                filePath = path;
                return Task.CompletedTask;
            };

            ISandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(
                options,
                configuration,
                xmlSerializer,
                xmlSerializerNamespaces,
                xmlWriterSettings,
                writeAllBytesAsync);

            await sandboxConfigurationBuilder.Build(mockLogonCommand);

            string expected = Path.Combine(options.RootFilesDirectoryLocation, options.SandboxConfigurationFileName);
            string actual = filePath;

            Assert.Equal(expected, actual);
        }
    }
}
