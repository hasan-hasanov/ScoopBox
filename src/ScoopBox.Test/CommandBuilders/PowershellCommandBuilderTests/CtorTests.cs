using ScoopBox.CommandBuilders;
using System;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace ScoopBox.Test.CommandBuilders.PowershellCommandBuilderTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeWithEmptyCtor()
        {
            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            Assert.True(powershellCommandBuilder != null);
        }

        [Fact]
        public void ShouldInitializeWithNullArguments()
        {
            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(null, null);

            Assert.True(powershellCommandBuilder != null);
        }

        [Fact]
        public void ShouldInitializeWithArguments()
        {
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(argsBefore, argsAfter);

            Assert.True(powershellCommandBuilder != null);
        }

        [Fact]
        public void ShouldInitializeWithArgumentsAndFileSystem()
        {
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };
            MockFileSystem mockFileSystem = new MockFileSystem();

            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder(argsBefore, argsAfter, mockFileSystem);

            Assert.True(powershellCommandBuilder != null);
        }

        [Fact]
        public void ShouldThrowExceptionWithoutFileSystem()
        {
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };

            Assert.Throws<ArgumentNullException>(() => new PowershellCommandBuilder(argsBefore, argsAfter, null));
        }
    }
}
