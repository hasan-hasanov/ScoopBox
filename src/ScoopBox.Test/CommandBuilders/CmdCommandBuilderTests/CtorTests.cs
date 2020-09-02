using ScoopBox.CommandBuilders;
using System;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace ScoopBox.Test.CommandBuilders.CmdCommandBuilderTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeWithEmptyCtor()
        {
            CmdCommandBuilder cmdCommandBuilder = new CmdCommandBuilder();

            Assert.True(cmdCommandBuilder != null);
        }

        [Fact]
        public void ShouldInitializeWithNullArguments()
        {
            CmdCommandBuilder cmdCommandBuilder = new CmdCommandBuilder(null, null);

            Assert.True(cmdCommandBuilder != null);
        }

        [Fact]
        public void ShouldInitializeWithArguments()
        {
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };

            CmdCommandBuilder cmdCommandBuilder = new CmdCommandBuilder(argsBefore, argsAfter);

            Assert.True(cmdCommandBuilder != null);
        }

        [Fact]
        public void ShouldInitializeWithArgumentsAndFileSystem()
        {
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };
            MockFileSystem mockFileSystem = new MockFileSystem();

            CmdCommandBuilder cmdCommandBuilder = new CmdCommandBuilder(argsBefore, argsAfter, mockFileSystem);

            Assert.True(cmdCommandBuilder != null);
        }

        [Fact]
        public void ShouldThrowExceptionWithoutFileSystem()
        {
            string[] argsBefore = new string[] { "-t", "Test" };
            string[] argsAfter = new string[] { "-r", "Rest" };

            Assert.Throws<ArgumentNullException>(() => new CmdCommandBuilder(argsBefore, argsAfter, null));
        }
    }
}
