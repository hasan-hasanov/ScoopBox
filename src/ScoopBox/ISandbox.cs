using ScoopBox.CommandBuilders;
using ScoopBox.PackageManager;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run(FileStream script, ICommandBuilder commandBuilder);

        Task Run(IDictionary<FileStream, ICommandBuilder> scripts);

        Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilder, IDictionary<IPackageManager, ICommandBuilder> packageManagers);

        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, FileStream scriptAfter, ICommandBuilder commandBuilderAfter);

        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, FileStream scriptAfter, ICommandBuilder commandBuilderAfter);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, IDictionary<FileStream, ICommandBuilder> scriptsAfter);

        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, IDictionary<FileStream, ICommandBuilder> scriptsAfter);
    }
}
