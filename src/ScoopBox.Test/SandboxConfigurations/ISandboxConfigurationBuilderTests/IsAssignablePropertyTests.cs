using ScoopBox.SandboxConfigurations;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.ISandboxConfigurationBuilderTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsOptionsAssignableToIOptions()
        {
            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(new Options());

            Assert.IsAssignableFrom<ISandboxConfigurationBuilder>(sandboxConfigurationBuilder);
        }
    }
}
