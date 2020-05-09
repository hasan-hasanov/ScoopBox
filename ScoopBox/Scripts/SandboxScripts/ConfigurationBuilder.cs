using ScoopBox.ConfigurationEntities;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.Collections.Generic;

namespace ScoopBox.Scripts.SandboxScripts
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private readonly ICommandBuilder commandBuilder;

        public ConfigurationBuilder(ICommandBuilder commandBuilder)
        {
            this.commandBuilder = commandBuilder;
        }

        public Configuration Build(ScoopBoxOptions options)
        {
            List<string> commands = this.commandBuilder.Build(options);
            Configuration configuration = new Configuration(options, commands);

            return configuration;
        }
    }
}
