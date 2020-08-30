using ScoopBox.CommandBuilders;
using Xunit;

namespace ScoopBox.Test.CommandBuilders.ICommandBuilderTests
{
    public class IsAssignablePropertyTests
    {
        [Fact]
        public void IsCmdCommandBuilderAssignableToICommandBuilder()
        {
            CmdCommandBuilder cmdCommandBuilder = new CmdCommandBuilder();

            Assert.IsAssignableFrom<ICommandBuilder>(cmdCommandBuilder);
        }

        [Fact]
        public void IsPowershellCommandBuilderAssignableToICommandBuilder()
        {
            PowershellCommandBuilder powershellCommandBuilder = new PowershellCommandBuilder();

            Assert.IsAssignableFrom<ICommandBuilder>(powershellCommandBuilder);
        }
    }
}
