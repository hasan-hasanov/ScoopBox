using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.SandboxConfigurationBuilderTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeWithOption()
        {
            IOptions options = new Options();

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);

            // We basically check here that no exception is thrown during initialization.
            Assert.True(true);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutOptions()
        {
            Assert.Throws<ArgumentNullException>(() => new SandboxConfigurationBuilder(null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutConfiguration()
        {
            IOptions options = new Options();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = async (path, content, token) => await File.WriteAllBytesAsync(path, content, token);

            Assert.Throws<ArgumentNullException>(() => new SandboxConfigurationBuilder(
                options,
                null,
                xmlSerializer,
                xmlSerializerNamespaces,
                xmlWriterSettings,
                writeAllBytesAsync));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutXmlSerializer()
        {
            IOptions options = new Options();
            Configuration configuration = new Configuration();
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = async (path, content, token) => await File.WriteAllBytesAsync(path, content, token);

            Assert.Throws<ArgumentNullException>(() => new SandboxConfigurationBuilder(
                options,
                configuration,
                null,
                xmlSerializerNamespaces,
                xmlWriterSettings,
                writeAllBytesAsync));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutXmlSerializerNamespaces()
        {
            IOptions options = new Options();
            Configuration configuration = new Configuration();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = async (path, content, token) => await File.WriteAllBytesAsync(path, content, token);

            Assert.Throws<ArgumentNullException>(() => new SandboxConfigurationBuilder(
                options,
                configuration,
                xmlSerializer,
                null,
                xmlWriterSettings,
                writeAllBytesAsync));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutXmlWriterSettings()
        {
            IOptions options = new Options();
            Configuration configuration = new Configuration();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            Func<string, byte[], CancellationToken, Task> writeAllBytesAsync = async (path, content, token) => await File.WriteAllBytesAsync(path, content, token);

            Assert.Throws<ArgumentNullException>(() => new SandboxConfigurationBuilder(
                options,
                configuration,
                xmlSerializer,
                xmlSerializerNamespaces,
                null,
                writeAllBytesAsync));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutWriteAllBytesAsync()
        {
            IOptions options = new Options();
            Configuration configuration = new Configuration();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            Assert.Throws<ArgumentNullException>(() => new SandboxConfigurationBuilder(
                options,
                configuration,
                xmlSerializer,
                xmlSerializerNamespaces,
                xmlWriterSettings,
                null));
        }
    }
}
