using System;
using System.IO;
using Xunit;

namespace ScoopBox.Test.PathResolversTests
{
    public class GetPackageManagerScriptsPathMethodTests
    {
        [Fact]
        public void ShouldReturnCorrectAfterScriptsPath()
        {
            string rootFilePath = @"C:/temp";
            string beforeScriptsFolder = "PackageManagerScripts";

            string expected = Path.Combine(rootFilePath, beforeScriptsFolder);
            string actual = PathResolvers.GetPackageManagerScriptsPath(rootFilePath);

            Assert.True(expected == actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ShouldThrowArgumentNullExceptionWhenRootFilePathIsNullOrEmptyOrWhiteSpace(string rootFilePath)
        {
            Assert.Throws<ArgumentNullException>(() => PathResolvers.GetPackageManagerScriptsPath(rootFilePath));
        }
    }
}
