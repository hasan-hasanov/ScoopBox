using ScoopBox.PackageManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading.Tasks;

namespace ScoopBox.CommandBuilders
{
    public class CmdCommandBuilder : ICommandBuilder
    {
        private readonly string[] _argsBefore;
        private readonly string[] _argsAfter;
        private readonly IFileSystem _fileSystem;

        public CmdCommandBuilder()
            : this(null, null)
        {
        }

        public CmdCommandBuilder(string[] argsBefore, string[] argsAfter)
            : this(argsBefore, argsAfter, new FileSystem())
        {
        }

        public CmdCommandBuilder(string[] argsBefore, string[] argsAfter, IFileSystem fileSystem)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            _argsBefore = argsBefore;
            _argsAfter = argsAfter;
            _fileSystem = fileSystem;
        }

        public Task<IEnumerable<string>> Build(FileStream file, string rootScriptFilesLocation, string rootSandboxScriptFilesLocation)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (string.IsNullOrWhiteSpace(rootScriptFilesLocation))
            {
                throw new ArgumentNullException(nameof(rootScriptFilesLocation));
            }

            if (string.IsNullOrWhiteSpace(rootSandboxScriptFilesLocation))
            {
                throw new ArgumentNullException(nameof(rootSandboxScriptFilesLocation));
            }

            string rootScriptFileFullName = Path.Combine(rootScriptFilesLocation, Path.GetFileName(file.Name));
            string sandboxScriptFileFullName = Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(file.Name));
            _fileSystem.File.Copy(file.Name, rootScriptFileFullName);

            StringBuilder sbCmdCommandBuilder = new StringBuilder();
            sbCmdCommandBuilder
                .Append("cmd.exe");

            if (_argsBefore?.Length > 0)
            {
                string beforeArguments = string.Join(" ", _argsBefore);

                sbCmdCommandBuilder
                    .Append(" ")
                    .Append(beforeArguments);
            }

            sbCmdCommandBuilder
                .Append(" ")
                .Append(sandboxScriptFileFullName);

            if (_argsAfter?.Length > 0)
            {
                string afterArguments = string.Join(" ", _argsAfter);

                sbCmdCommandBuilder
                    .Append(" ")
                    .Append(afterArguments);
            }

            return Task.FromResult<IEnumerable<string>>(new List<string>() { sbCmdCommandBuilder.ToString() });
        }

        public async Task<IEnumerable<string>> Build(IPackageManager packageManager, string rootScriptFilesLocation, string rootSandboxScriptFilesLocation)
        {
            if (packageManager == null)
            {
                throw new ArgumentNullException(nameof(packageManager));
            }

            if (string.IsNullOrWhiteSpace(rootScriptFilesLocation))
            {
                throw new ArgumentNullException(nameof(rootScriptFilesLocation));
            }

            if (string.IsNullOrWhiteSpace(rootSandboxScriptFilesLocation))
            {
                throw new ArgumentNullException(nameof(rootSandboxScriptFilesLocation));
            }

            string rootScriptFileFullName = await packageManager.GenerateScriptFile(rootScriptFilesLocation);
            string sandboxScriptFileFullName = Path.Combine(rootSandboxScriptFilesLocation, Path.GetFileName(rootScriptFileFullName));

            StringBuilder sbCmdCommandBuilder = new StringBuilder();
            sbCmdCommandBuilder
                .Append("cmd.exe");

            if (_argsBefore?.Length > 0)
            {
                string beforeArguments = string.Join(" ", _argsBefore);

                sbCmdCommandBuilder
                    .Append(" ")
                    .Append(beforeArguments);
            }

            sbCmdCommandBuilder
                .Append(" ")
                .Append(sandboxScriptFileFullName);

            if (_argsAfter?.Length > 0)
            {
                string afterArguments = string.Join(" ", _argsAfter);

                sbCmdCommandBuilder
                    .Append(" ")
                    .Append(afterArguments);
            }

            return new List<string>() { sbCmdCommandBuilder.ToString() };
        }

        public Task<IEnumerable<string>> Build(string literalScript)
        {
            StringBuilder sbCmdCommandBuilder = new StringBuilder();
            sbCmdCommandBuilder
                .Append("cmd.exe");

            if (_argsBefore?.Length > 0)
            {
                string beforeArguments = string.Join(" ", _argsBefore);

                sbCmdCommandBuilder
                    .Append(" ")
                    .Append(beforeArguments);
            }

            if (!string.IsNullOrWhiteSpace(literalScript))
            {
                sbCmdCommandBuilder
                    .Append(" ")
                    .Append(literalScript);
            }

            if (_argsAfter?.Length > 0)
            {
                string afterArguments = string.Join(" ", _argsAfter);

                sbCmdCommandBuilder
                    .Append(" ")
                    .Append(afterArguments);
            }

            return Task.FromResult<IEnumerable<string>>(new List<string>() { sbCmdCommandBuilder.ToString() });
        }
    }
}
