using ScoopBox.PackageManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading.Tasks;

namespace ScoopBox.CommandBuilders
{
    public class PowershellCommandBuilder : ICommandBuilder
    {
        private readonly string[] _argsBefore;
        private readonly string[] _argsAfter;
        private readonly IFileSystem _fileSystem;

        public PowershellCommandBuilder()
            : this(null, null)
        {
        }

        public PowershellCommandBuilder(string[] argsBefore, string[] argsAfter)
            : this(argsBefore, argsAfter, new FileSystem())
        {
        }

        public PowershellCommandBuilder(string[] argsBefore, string[] argsAfter, IFileSystem fileSystem)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            _argsBefore = argsBefore;
            _argsAfter = argsAfter;
            _fileSystem = fileSystem;
        }

        public Task<IEnumerable<string>> Build(FileSystemInfo file, string rootScriptFilesLocation, string rootSandboxScriptFilesLocation)
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

            string rootScriptFileFullName = Path.Combine(rootScriptFilesLocation, file.Name);
            string sandboxScriptFileFullName = Path.Combine(rootSandboxScriptFilesLocation, file.Name);
            _fileSystem.File.Copy(file.FullName, rootScriptFileFullName);

            StringBuilder sbPowershellCommandBuilder = new StringBuilder();
            sbPowershellCommandBuilder
                .Append("powershell.exe");

            if (_argsBefore?.Length > 0)
            {
                string beforeArguments = string.Join(" ", _argsBefore);

                sbPowershellCommandBuilder
                    .Append(" ")
                    .Append(beforeArguments);
            }

            sbPowershellCommandBuilder
                .Append(" ")
                .Append(sandboxScriptFileFullName);

            if (_argsAfter?.Length > 0)
            {
                string afterArguments = string.Join(" ", _argsAfter);

                sbPowershellCommandBuilder
                    .Append(" ")
                    .Append(afterArguments);
            }

            string executionPolicyCommand = $"powershell.exe -ExecutionPolicy Bypass -File {sandboxScriptFileFullName}";
            string powershellCommand = sbPowershellCommandBuilder.ToString();

            return Task.FromResult<IEnumerable<string>>(new List<string>() { executionPolicyCommand, powershellCommand });
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

            StringBuilder sbPowershellCommandBuilder = new StringBuilder();
            sbPowershellCommandBuilder
                .Append("powershell.exe");

            if (_argsBefore?.Length > 0)
            {
                string beforeArguments = string.Join(" ", _argsBefore);

                sbPowershellCommandBuilder
                    .Append(" ")
                    .Append(beforeArguments);
            }

            sbPowershellCommandBuilder
                .Append(" ")
                .Append(sandboxScriptFileFullName);

            if (_argsAfter?.Length > 0)
            {
                string afterArguments = string.Join(" ", _argsAfter);

                sbPowershellCommandBuilder
                    .Append(" ")
                    .Append(afterArguments);
            }

            string executionPolicyCommand = $"powershell.exe -ExecutionPolicy Bypass -File {sandboxScriptFileFullName}";
            string powershellCommand = sbPowershellCommandBuilder.ToString();

            return new List<string>() { executionPolicyCommand, powershellCommand };
        }

        public Task<IEnumerable<string>> Build(string literalScript)
        {
            StringBuilder sbPowershellCommandBuilder = new StringBuilder();
            sbPowershellCommandBuilder
                .Append("powershell.exe");

            if (_argsBefore?.Length > 0)
            {
                string beforeArguments = string.Join(" ", _argsBefore);

                sbPowershellCommandBuilder
                    .Append(" ")
                    .Append(beforeArguments);
            }

            if (!string.IsNullOrWhiteSpace(literalScript))
            {
                sbPowershellCommandBuilder
                    .Append(" ")
                    .Append(literalScript);
            }

            if (_argsAfter?.Length > 0)
            {
                string afterArguments = string.Join(" ", _argsAfter);

                sbPowershellCommandBuilder
                    .Append(" ")
                    .Append(afterArguments);
            }

            return Task.FromResult<IEnumerable<string>>(new List<string>() { sbPowershellCommandBuilder.ToString() });
        }
    }
}
