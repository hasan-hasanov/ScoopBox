using ScoopBox.Entities;
using ScoopBox.Scripts.SandboxScripts.Abstract;
using System.Collections.Generic;

namespace ScoopBox.Scripts.SandboxScripts
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private readonly ICommandBuilder commandBuilder;
        private readonly IMappedFoldersBuilder mappedFoldersBuilder;

        public ConfigurationBuilder(
            ICommandBuilder commandBuilder,
            IMappedFoldersBuilder mappedFoldersBuilder)
        {
            this.commandBuilder = commandBuilder;
            this.mappedFoldersBuilder = mappedFoldersBuilder;
        }

        public Configuration Build(ScoopBoxOptions options)
        {
            List<string> commands = this.commandBuilder.Build(options);
            MappedFolders mappedFolders = this.mappedFoldersBuilder.Build(options);

            Configuration configuration = new Configuration(options, commands, mappedFolders);

            return configuration;
        }
    }
}
