using ScoopBox.SandboxConfigurations;
using Xunit;

namespace ScoopBox.Test.SandboxConfigurations.ISandboxConfigurationBuilderTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsSandboxConfigurationBuilderAssignableToISandboxConfigurationBuilder()
        {
            IOptions options = new Options();

            SandboxConfigurationBuilder sandboxConfigurationBuilder = new SandboxConfigurationBuilder(options);

            Assert.IsAssignableFrom<ISandboxConfigurationBuilder>(sandboxConfigurationBuilder);
        }
    }
}
