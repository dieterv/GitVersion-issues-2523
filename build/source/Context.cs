using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

public class Context : FrostingContext
{
    public Context(ICakeContext context)
        : base(context)
    {
    }

    // Arguments
    public string Target { get; set; }

    public new string Configuration { get; set; }

    // Directory layout
    public DirectoryPath RootDir { get; set; }

    public DirectoryPath AssetsDir { get; set; }

    public DirectoryPath BuildDir { get; set; }

    public DirectoryPath ArtifactsDir { get; set; }

    public DirectoryPath TestResultsDir { get; set; }

    public DirectoryPath CoverageDir { get; set; }

    public DirectoryPath PackagesDir { get; set; }

    public DirectoryPath SourceDir { get; set; }

    public DirectoryPath ToolsDir { get; set; }

    // Project information
    public string Title { get; set; }

    public FilePath SolutionFile { get; set; }

    // Build information
    public bool IsCiBuild { get; set; }

    public bool IsMainProjectRepo { get; set; }

    public bool IsTagged { get; set; }

    public bool IsReleasableBranch { get; set; }

    public bool IsReleaseBuild { get; set; }

    // MSBuild debugging
    public bool MsBuildDebug { get; set; }

    public FilePath MsBuildBuildLogFile { get; set; }

    public FilePath MsBuildTestLogFile { get; set; }

    public FilePath MsBuildPackLogFile { get; set; }

    // Coverage
    public string CoverageFileName { get; set; }

    public FilePath CoverageFile { get; set; }

    // Packaging
    public string NugetPackageFeed { get; set; }

    public bool ShouldPublish { get; set; }
}
