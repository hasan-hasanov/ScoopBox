using ScoopBox.Translators.Cmd;
using System;
using Xunit;

namespace ScoopBox.Test.Translators.Cmd.CmdTranslatorTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeWithoutArguments()
        {
            CmdTranslator translator = new CmdTranslator();

            // This is checking that the class is initialized properly and does not throw exception.
            Assert.True(true);
        }

        [Fact]
        public void ShouldInitializeWithArguments()
        {
            CmdTranslator translator = new CmdTranslator(new string[] { "-p", "test" });

            // This is checking that the class is initialized properly and does not throw exception.
            Assert.True(true);
        }

        [Fact]
        public void ShouldThrowExceptionWithoutGetTicks()
        {
            Assert.Throws<ArgumentNullException>(() => new CmdTranslator(new string[] { "-p", "test" }, null));
        }
    }
}
