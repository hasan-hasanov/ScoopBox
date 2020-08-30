using Newtonsoft.Json;
using ScoopBox.CommandBuilders;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScoopBox.Test.CommandBuilders.PowershellCommandBuilderTests
{
    public class BuildMethodTests
    {
        [Fact]
        public void ShouldReturnCorrectStringWithSingleParameter()
        {
            string fullScriptFilePath = @"C://Users/test.ps1";

            IEnumerable<string> expectedRaw = new List<string>() {
                $"powershell.exe -ExecutionPolicy Bypass -File {fullScriptFilePath}",
                $"powershell.exe {fullScriptFilePath}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();
            IEnumerable<string> actualRaw = powershellCommandBuilder.Build(fullScriptFilePath);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public void ShouldReturnCorrectStringWithArgumentsBeforeParameter()
        {
            string fullScriptFilePath = @"C://Users/test.ps1";
            string[] argumentsBefore = new string[] { "-t", "Test" };

            IEnumerable<string> expectedRaw = new List<string>() {
                $"powershell.exe -ExecutionPolicy Bypass -File {fullScriptFilePath}",
                $"powershell.exe {argumentsBefore[0]} {argumentsBefore[1]} {fullScriptFilePath}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();
            IEnumerable<string> actualRaw = powershellCommandBuilder.Build(fullScriptFilePath, argumentsBefore, null);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public void ShouldReturnCorrectStringWithArgumentsAfterParameter()
        {
            string fullScriptFilePath = @"C://Users/test.ps1";
            string[] argumentsAfter = new string[] { "-t", "Test" };

            IEnumerable<string> expectedRaw = new List<string>() {
                $"powershell.exe -ExecutionPolicy Bypass -File {fullScriptFilePath}",
                $"powershell.exe {fullScriptFilePath} {argumentsAfter[0]} {argumentsAfter[1]}"
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();
            IEnumerable<string> actualRaw = powershellCommandBuilder.Build(fullScriptFilePath, null, argumentsAfter);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public void ShouldReturnCorrectStringWithArgumentsBeforeAndAfterParameter()
        {
            string fullScriptFilePath = @"C://Users/test.ps1";
            string[] argumentsBefore = new string[] { "-r", "Rest" };
            string[] argumentsAfter = new string[] { "-t", "Test" };

            IEnumerable<string> expectedRaw = new List<string>() {
                $"powershell.exe -ExecutionPolicy Bypass -File {fullScriptFilePath}",
                $"powershell.exe {argumentsBefore[0]} {argumentsBefore[1]} {fullScriptFilePath} {argumentsAfter[0]} {argumentsAfter[1]}" 
            };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();
            IEnumerable<string> actualRaw = powershellCommandBuilder.Build(fullScriptFilePath, argumentsBefore, argumentsAfter);

            string expectedSerialized = JsonConvert.SerializeObject(expectedRaw);
            string actualSerialized = JsonConvert.SerializeObject(actualRaw);

            Assert.True(expectedSerialized == actualSerialized);
        }

        [Fact]
        public void ShouldThrowExceptionOnIncorrectData()
        {
            string fullScriptFilePath = null;

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            Assert.Throws<ArgumentNullException>(() => powershellCommandBuilder.Build(fullScriptFilePath));
        }
    }
}
