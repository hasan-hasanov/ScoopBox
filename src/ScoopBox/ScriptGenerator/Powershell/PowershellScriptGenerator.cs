using ScoopBox.ScriptGenerator;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace ScoopBox.ScriptBuilders.Powershell
{
    public class PowershellScriptGenerator : IScriptGenerator
    {
        private readonly IFileSystem _fileSystem;

        public string ScriptName => "PowershellScriptContainer.ps1";

        public PowershellScriptGenerator()
            : this(new FileSystem())
        {
        }

        public PowershellScriptGenerator(IFileSystem fileSystem)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            _fileSystem = fileSystem;
        }

        public async Task<FileSystemInfo> Generate(string path, string content)
        {
            string fullScriptPath = Path.Combine(path, ScriptName);
            using (StreamWriter writer = _fileSystem.File.CreateText(fullScriptPath))
            {
                await writer.WriteAsync(content);
            }

            return new FileInfo(fullScriptPath);
        }
    }
}
