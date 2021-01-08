using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Frosting;

[TaskName("Clean")]
public sealed class CleanTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information(context.Environment.WorkingDirectory);

        // Clean directories
        context.CleanDirectories("./source/**/bin");
        context.CleanDirectories("./source/**/obj");
        context.CleanDirectories(context.ArtifactsDir.FullPath);
        context.CleanDirectories(context.CoverageDir.FullPath);
        context.CleanDirectories(context.TestResultsDir.FullPath);
        context.CleanDirectories(context.PackagesDir.FullPath);

        // Ensure key directories exist
        context.CreateDirectory(context.ArtifactsDir);
        context.CreateDirectory(context.CoverageDir);
        context.CreateDirectory(context.TestResultsDir);
        context.CreateDirectory(context.PackagesDir);
    }
}
