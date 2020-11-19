using ScoopBox.SandboxProcesses.ProcessAdapters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ScoopBox.SandboxProcesses
{
    public class SandboxCmdProcess : ISandboxProcess
    {
        private const string CMD_EXE = "cmd.exe";

        private readonly string _configurationFileLocation;
        private readonly IProcessAdapter _processAdapter;

        public SandboxCmdProcess()
            : this(
                  $"{Path.GetTempPath()}/Sandbox",
                  "sandbox.wsb")
        {
        }

        public SandboxCmdProcess(string rootFilesDirectoryLocation, string sandboxConfigurationFileName)
            : this(rootFilesDirectoryLocation, sandboxConfigurationFileName, new ProcessAdapter(CMD_EXE))
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

        public async Task StartAsync()
        {
            _processAdapter.Start();
            await _processAdapter.StandardInputWriteLineAsync($"\"{_configurationFileLocation}\"");
            await _processAdapter.StandardInputFlushAsync();
            _processAdapter.StandardInputClose();
            _processAdapter.WaitForExit();
        }
    }
}
