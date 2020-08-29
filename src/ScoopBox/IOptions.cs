namespace ScoopBox
{
    public interface IOptions
    {
        string SandboxBeforeScriptsLocation { get; }

        string SandboxAfterScriptsLocation { get; }

        string SandboxPackageManagerScriptsLocation { get; }

        string RootFilesDirectoryLocation { get; }

        string SandboxConfigurationFileName { get; }
    }
}
