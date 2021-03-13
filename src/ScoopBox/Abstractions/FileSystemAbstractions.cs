using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ScoopBox
{
    internal static class FileSystemAbstractions
    {
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void DeleteFilesInDirectory(string directoryPath)
        {
            foreach (var file in new DirectoryInfo(directoryPath).EnumerateFiles())
            {
                file.Delete();
            }
        }

        public static void DeleteDirectoriesInDirectory(string directoryPath)
        {
            foreach (var directory in new DirectoryInfo(directoryPath).EnumerateDirectories())
            {
                directory.Delete(true);
            }
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static async Task WriteAllBytesAsync(string scriptPath, byte[] content, CancellationToken token)
        {
            await File.WriteAllBytesAsync(scriptPath, content, token);
        }

        public static async Task CopyFileToDestination(string source, string destination, CancellationToken token)
        {
            using FileStream sourceStream = File.Open(source, FileMode.Open);
            using FileStream destinationStream = File.Create(destination);
            await sourceStream.CopyToAsync(destinationStream, token);
        }
    }
}
