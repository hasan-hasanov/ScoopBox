using ScoopBox.Translators.Bat;
using System;
using Xunit;

namespace ScoopBox.Test.Translators.Bat.BatTranslatorTests
{
    public class CtorTests
    {
        [Fact]
        public void ShouldInitializeWithoutArguments()
        {
            BatTranslator translator = new BatTranslator();

            // This is checking that the class is initialized properly and does not throw exception.
            Assert.True(true);
        }

        [Fact]
        public void ShouldInitializeWithArguments()
        {
            BatTranslator translator = new BatTranslator(new string[] { "-p", "test" });

            // This is checking that the class is initialized properly and does not throw exception.
            Assert.True(true);
        }

        [Fact]
        public void ShouldThrowExceptionWithoutGetTicks()
        {
            Assert.Throws<ArgumentNullException>(() => new BatTranslator(new string[] { "-p", "test" }, null));
        }
    }
}
