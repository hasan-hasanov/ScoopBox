using Xunit;

namespace ScoopBox.Test.ISandboxTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsSandboxAssignableToISandbox()
        {
            Sandbox sandbox = new Sandbox();

            Assert.IsAssignableFrom<ISandbox>(sandbox);
        }
    }
}
