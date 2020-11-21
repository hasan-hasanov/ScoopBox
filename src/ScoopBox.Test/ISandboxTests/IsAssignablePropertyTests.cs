using Xunit;

namespace ScoopBox.Test.ISandboxTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void ISandboxAssignableToSandbox()
        {
            Sandbox sandbox = new Sandbox();

            Assert.IsAssignableFrom<ISandbox>(sandbox);
        }
    }
}
