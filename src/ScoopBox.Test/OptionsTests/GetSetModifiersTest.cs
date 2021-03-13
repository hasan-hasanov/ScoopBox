using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ScoopBox.Test.OptionsTests
{
    public class GetSetModifiersTest
    {
        [Fact]
        public void ShouldReturnDefatultValues()
        {
            var sandboxDesktopLocation = @"C:\Users\WDAGUtilityAccount\Desktop\";
            var sandboxConfigurationFileName = "sandbox.wsb";
            var rootSandboxFilesDirectoryLocation = @"C:\Users\WDAGUtilityAccount\Desktop\Sandbox\";
            var rootFilesDirectoryLocation = $"{Path.GetTempPath()}Sandbox";
            var vGpu = VGpuOptions.Disabled;
            var networking = NetworkingOptions.Default;
            var audioInput = AudioInputOptions.Default;
            var videoInput = VideoInputOptions.Default;
            var protectedClient = ProtectedClientOptions.Default;
            var printerRedirection = PrinterRedirectionOptions.Default;
            var clipboardRedirection = ClipboardRedirectionOptions.Default;
            var memoryInMB = 0;
            var userMappedDirectories = new List<MappedFolder>();

            Options options = new Options();

            Assert.Equal(sandboxDesktopLocation, options.SandboxDesktopLocation);
            Assert.Equal(sandboxConfigurationFileName, options.SandboxConfigurationFileName);
            Assert.Equal(rootSandboxFilesDirectoryLocation, options.RootSandboxFilesDirectoryLocation);
            Assert.Equal(rootFilesDirectoryLocation, options.RootFilesDirectoryLocation);
            Assert.Equal(vGpu, options.VGpu);
            Assert.Equal(networking, options.Networking);
            Assert.Equal(audioInput, options.AudioInput);
            Assert.Equal(videoInput, options.VideoInput);
            Assert.Equal(protectedClient, options.ProtectedClient);
            Assert.Equal(printerRedirection, options.PrinterRedirection);
            Assert.Equal(clipboardRedirection, options.ClipboardRedirection);
            Assert.Equal(memoryInMB, options.MemoryInMB);
            Assert.Equal(userMappedDirectories, options.UserMappedDirectories);
        }
    }
}
