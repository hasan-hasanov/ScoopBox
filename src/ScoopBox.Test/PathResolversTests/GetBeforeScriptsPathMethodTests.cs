using System;
using System.IO;
using Xunit;

namespace ScoopBox.Test.PathResolversTests
{
    public class GetBeforeScriptsPathMethodTests
    {
        [Fact]
        public void ShouldReturnCorrectBeforeScriptsPath()
        {
            string rootFilePath = @"C:/temp";
            string beforeScriptsFolder = "BeforeScripts";

            string expected = Path.Combine(rootFilePath, beforeScriptsFolder);
            string actual = PathResolvers.GetBeforeScriptsPath(rootFilePath);

            Assert.True(expected == actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ShouldThrowArgumentNullExceptionWhenRootFilePathIsNullOrEmptyOrWhiteSpace(string rootFilePath)
        {
            Assert.Throws<ArgumentNullException>(() => PathResolvers.GetBeforeScriptsPath(rootFilePath));
        }
    }
}
