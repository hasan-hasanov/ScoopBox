using System.IO;

namespace ScoopBox.Test.Mocks
{
    public class MockFileSystemInfo : FileSystemInfo
    {
        private string _fullName;

        public MockFileSystemInfo(string fullName)
        {
            _fullName = fullName;
        }

        public override bool Exists => false;

        public override string Name => Path.GetFileName(_fullName);

        public override string FullName => _fullName;

        public override void Delete()
        {
        }
    }
}
