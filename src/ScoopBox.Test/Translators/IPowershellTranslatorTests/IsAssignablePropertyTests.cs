using Xunit;

namespace ScoopBox.Test.Translators.IPowershellTranslatorTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsOptionsAssignableToIOptions()
        {
            PowershellTranslator powershellTranslator = new PowershellTranslator();

            Assert.IsAssignableFrom<IPowershellTranslator>(powershellTranslator);
        }
    }
}
