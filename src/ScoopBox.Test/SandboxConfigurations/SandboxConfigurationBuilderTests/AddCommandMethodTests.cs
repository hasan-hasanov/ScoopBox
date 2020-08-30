using Newtonsoft.Json;
using ScoopBox.SandboxConfigurations;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.SandboxConfigurationBuilderTests
{
    public class AddCommandMethodTests
    {
        [Fact]
        public void ShouldAddCommandToCollection()
        {
            IOptions options = new Options();
            string command = @"powershell.exe C:/temp/script.ps1";

            IList<string> expectedRaw = new List<string>() { command };

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);
            sandboxConfigurationBuilder.AddCommand(command);

            IList<string> actualRaw = sandboxConfigurationBuilder.Commands;

            string expected = JsonConvert.SerializeObject(expectedRaw);
            string actual = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expected == actual);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWithoutCommand()
        {
            IOptions options = new Options();
            string command = null;

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);

            Assert.Throws<ArgumentNullException>(() => sandboxConfigurationBuilder.AddCommand(command));
        }
    }
}
