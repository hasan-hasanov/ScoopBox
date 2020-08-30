using Newtonsoft.Json;
using ScoopBox.SandboxConfigurations;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.SandboxConfigurationBuilderTests
{
    public class AddCommandsMethodTests
    {
        [Fact]
        public void ShouldAddCommandsToCollection()
        {
            IOptions options = new Options();
            IList<string> commands = new List<string>()
            {
                @"powershell.exe C:/temp/script.ps1",
                @"powershell.exe C:/temp/script2.ps1"
            };

            IList<string> expectedRaw = commands;

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);
            sandboxConfigurationBuilder.AddCommands(commands);

            IList<string> actualRaw = sandboxConfigurationBuilder.Commands;

            string expected = JsonConvert.SerializeObject(expectedRaw);
            string actual = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expected == actual);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutCommands()
        {
            IOptions options = new Options();
            IList<string> commands = null;

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);

            Assert.Throws<ArgumentNullException>(() => sandboxConfigurationBuilder.AddCommands(commands));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyCommands()
        {
            IOptions options = new Options();
            IList<string> commands = new List<string>();

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);

            Assert.Throws<ArgumentException>(() => sandboxConfigurationBuilder.AddCommands(commands));
        }
    }
}
