using ScoopBox.Scripts.Abstract;
using System.Text;

namespace ScoopBox.Scripts
{
    public class SetExecutionPolicy : ISetExecutionPolicy
    {
        public string Set()
        {
            StringBuilder executionPolicy = new StringBuilder();

            executionPolicy.Append("Set-ExecutionPolicy ");
            executionPolicy.Append("RemoteSigned ");
            executionPolicy.Append("-scope");
            executionPolicy.Append("CurrentUser");

            return executionPolicy.ToString();
        }
    }
}
