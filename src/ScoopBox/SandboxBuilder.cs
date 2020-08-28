using ScoopBox.Entities;

namespace ScoopBox
{
    public class SandboxBuilder : ISandboxScriptBuilder
    {
        private readonly Configuration _configuration;

        public SandboxBuilder()
        {
            _configuration = new Configuration();
        }

        public void BuildExecutionPolicy()
        {
            throw new System.NotImplementedException();
        }

        public void BuildExecutionScript()
        {
            throw new System.NotImplementedException();
        }

        public void BuildMappedFolders()
        {
            throw new System.NotImplementedException();
        }

        public Configuration Build()
        {
            return this._configuration;
        }
    }
}
