using Xunit;

namespace ScoopBox.Test.IOptionsTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsOptionsAssignableToIOptions()
        {
            Options options = new Options();

            Assert.IsAssignableFrom<IOptions>(options);
        }
    }
}
