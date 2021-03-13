using System;
using Xunit;

namespace ScoopBox.Test.Translators.PowershellTranslatorTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeWithoutArguments()
        {
            PowershellTranslator translator = new PowershellTranslator();

            // This is checking that the class is initialized properly and does not throw exception.
            Assert.True(true);
        }

        [Fact]
        public void ShouldInitializeWithArguments()
        {
            PowershellTranslator translator = new PowershellTranslator(new string[] { "-p", "test" });

            // This is checking that the class is initialized properly and does not throw exception.
            Assert.True(true);
        }

        [Fact]
        public void ShouldThrowExceptionWithoutGetTicks()
        {
            Assert.Throws<ArgumentNullException>(() => new PowershellTranslator(new string[] { "-p", "test" }, null));
        }
    }
}
