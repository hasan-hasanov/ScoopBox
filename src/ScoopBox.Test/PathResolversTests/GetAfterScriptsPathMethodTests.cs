using System;
using System.IO;
using Xunit;

namespace ScoopBox.Test.PathResolversTests
{
    public class GetAfterScriptsPathMethodTests
    {
        [Fact]
        public void ShouldReturnCorrectAfterScriptsPath()
        {
            string rootFilePath = @"C:/temp";
            string beforeScriptsFolder = "AfterScripts";

            string expected = Path.Combine(rootFilePath, beforeScriptsFolder);
            string actual = PathResolvers.GetAfterScriptsPath(rootFilePath);

            Assert.True(expected == actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ShouldThrowArgumentNullExceptionWhenRootFilePathIsNullOrEmptyOrWhiteSpace(string rootFilePath)
        {
            Assert.Throws<ArgumentNullException>(() => PathResolvers.GetAfterScriptsPath(rootFilePath));
        }
    }
}
