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

        Task Run(HashSet<IPackageManager> applications);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilder, HashSet<IPackageManager> applications);

        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, HashSet<IPackageManager> applications);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, HashSet<IPackageManager> applications, FileStream scriptAfter, ICommandBuilder commandBuilderAfter);
        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, HashSet<IPackageManager> applications, FileStream scriptAfter, ICommandBuilder commandBuilderAfter);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, HashSet<IPackageManager> applications, IDictionary<FileStream, ICommandBuilder> scriptsAfter);

        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, HashSet<IPackageManager> applications, IDictionary<FileStream, ICommandBuilder> scriptsAfter);
    }
}
