using ScoopBox.Scripts.InstallerScripts.Abstract;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.Collections.Generic;

namespace ScoopBox.Scripts.SandboxScripts
{
    public class CommandBuilder : ICommandBuilder
    {
        private readonly IExecutionPolicyCommandBuilder executionPolicyBuilder;
        private readonly IExecutionScriptCommandBuilder executionScriptCommandBuilder;

        public CommandBuilder(
            IExecutionPolicyCommandBuilder executionPolicyBuilder,
            IExecutionScriptCommandBuilder executionScriptCommandBuilder)
        {
            this.executionPolicyBuilder = executionPolicyBuilder;
            this.executionScriptCommandBuilder = executionScriptCommandBuilder;
        }

        public List<string> Build(ScoopBoxOptions options)
        {
            string executionPolicyBuilder = this.executionPolicyBuilder.Build(options);
            string executionScriptBuilder = this.executionScriptCommandBuilder.Build();

            return new List<string>()
            {
                executionPolicyBuilder,
                executionScriptBuilder
            };
        }
    }
}
