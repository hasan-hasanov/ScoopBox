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

        Task Run(IDictionary<string, IPackageManager> applications);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilder, IDictionary<string, IPackageManager> applications);

        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, IDictionary<string, IPackageManager> applications);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<string, IPackageManager> applications, FileStream scriptAfter, ICommandBuilder commandBuilderAfter);
        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, IDictionary<string, IPackageManager> applications, FileStream scriptAfter, ICommandBuilder commandBuilderAfter);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<string, IPackageManager> applications, IDictionary<FileStream, ICommandBuilder> scriptsAfter);

        Task Run(IDictionary<FileStream, ICommandBuilder> scriptsBefore, IDictionary<string, IPackageManager> applications, IDictionary<FileStream, ICommandBuilder> scriptsAfter);
    }
}
