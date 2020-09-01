using ScoopBox.CommandBuilders;
using ScoopBox.PackageManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox
{
    public interface ISandbox
    {
        Task Run(string literalScript, ICommandBuilder commandBuilder);

        Task Run(FileStream script, ICommandBuilder commandBuilder);

        Task Run(List<Tuple<string, ICommandBuilder>> literalScripts);

        Task Run(List<Tuple<FileStream, ICommandBuilder>> scripts);

        Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers);

        Task Run(string literalScriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers);

        Task Run(List<Tuple<string, ICommandBuilder>> literalScriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers);

        Task Run(List<Tuple<FileStream, ICommandBuilder>> scriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers);

        Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers, string literalScriptAfter, ICommandBuilder commandBuilderAfter);

        Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers, FileStream scriptAfter, ICommandBuilder commandBuilderAfter);

        Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers, List<Tuple<string, ICommandBuilder>> literalScriptsAfter);

        Task Run(IDictionary<IPackageManager, ICommandBuilder> packageManagers, List<Tuple<FileStream, ICommandBuilder>> scriptsAfter);

        Task Run(string literalScriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, string literalScriptAfter, ICommandBuilder commandBuilderAfter);

        Task Run(FileStream scriptBefore, ICommandBuilder commandBuilderBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, FileStream scriptAfter, ICommandBuilder commandBuilderAfter);

        Task Run(List<Tuple<string, ICommandBuilder>> literalScriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, List<Tuple<string, ICommandBuilder>> literalScriptsAfter);

        Task Run(List<Tuple<FileStream, ICommandBuilder>> scriptsBefore, IDictionary<IPackageManager, ICommandBuilder> packageManagers, List<Tuple<FileStream, ICommandBuilder>> scriptsAfter);
    }
}
