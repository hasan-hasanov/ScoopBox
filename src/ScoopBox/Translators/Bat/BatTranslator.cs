using ScoopBox.Abstractions;
using System;
using System.IO;
using System.Text;

namespace ScoopBox.Translators.Bat
{
    public class BatTranslator : IPowershellTranslator
    {
        private readonly string[] _argumentsAfter;
        private readonly Func<long> _getTicks;

        public BatTranslator()
            : this(null)
        {
        }

        public BatTranslator(string[] argumentsAfter)
            : this(argumentsAfter, DateTimeAbstractions.GetTicks)
        {
        }

        internal BatTranslator(string[] argumentsAfter, Func<long> getTicks)
        {
            if (getTicks == null)
            {
                throw new ArgumentNullException(nameof(getTicks));
            }

            _argumentsAfter = argumentsAfter;
            _getTicks = getTicks;
        }

        public string Translate(FileSystemInfo file, IOptions options)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            string sandboxScriptFileFullName = Path.Combine(options.RootSandboxFilesDirectoryLocation, file.Name);

            StringBuilder sbPowershellCommandBuilder = new StringBuilder()
                .Append($@"powershell.exe ""{ sandboxScriptFileFullName }""")
                .Append(" ")
                .Append($@"3>&1 2>&1 > ""{Path.Combine(options.SandboxDesktopLocation, $"Log_{_getTicks()}.txt")}""");

            if (_argumentsAfter?.Length > 0)
            {
                sbPowershellCommandBuilder.Append(" ").Append(string.Join(" ", _argumentsAfter));
            }

            return sbPowershellCommandBuilder.ToString();
        }
    }
}
