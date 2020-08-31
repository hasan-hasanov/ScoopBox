using ScoopBox.SandboxProcesses.ProcessAdapters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses.Cmd
{
    public class SandboxCmdProcess : ISandboxProcess
    {
        private readonly string _configurationFileLocation;
        private readonly IProcessAdapter _processAdapter;

        public SandboxCmdProcess()
            : this(
                  $"{Path.GetTempPath()}/Sandbox",
                  "sandbox.wsb")
        {
        }

        public SandboxCmdProcess(string rootFilesDirectoryLocation, string sandboxConfigurationFileName)
            : this(rootFilesDirectoryLocation, sandboxConfigurationFileName, new ProcessAdapter())
        {
        }

        public SandboxCmdProcess(string rootFilesDirectoryLocation, string sandboxConfigurationFileName, IProcessAdapter processAdapter)
        {
            if (string.IsNullOrWhiteSpace(rootFilesDirectoryLocation))
            {
                throw new ArgumentNullException(nameof(rootFilesDirectoryLocation));
            }

            if (string.IsNullOrWhiteSpace(sandboxConfigurationFileName))
            {
                throw new ArgumentNullException(nameof(sandboxConfigurationFileName));
            }

            if (processAdapter == null)
            {
                throw new ArgumentNullException(nameof(processAdapter));
            }

            _configurationFileLocation = Path.Combine(rootFilesDirectoryLocation, sandboxConfigurationFileName);
            _processAdapter = processAdapter;
        }

        public string ProcessName => "cmd.exe";

        public async Task StartAsync()
        {
            _processAdapter.Start(ProcessName);
            await _processAdapter.StandartInputWriteLine($"\"{_configurationFileLocation}\"");
            _processAdapter.WaitForExit();
        }
    }
}
