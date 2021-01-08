using System;
using System.Text;

using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;

public sealed class Lifetime : FrostingLifetime<Context>
{
    public override void Setup(Context context)
    {
        context.Information("Setting things up...");

        // Arguments
        context.Target = context.Argument("target", "Default");
        context.Configuration = context.Argument("configuration", "Release");

        // Directory layout
        context.RootDir = context.MakeAbsolute(context.Directory("."));
        context.AssetsDir = context.RootDir.Combine("assets");
        context.BuildDir = context.RootDir.Combine("build");
        context.ArtifactsDir = context.BuildDir.Combine("artifacts");
        context.CoverageDir = context.ArtifactsDir.Combine("coverage");
        context.TestResultsDir = context.ArtifactsDir.Combine("test-results");
        context.PackagesDir = context.ArtifactsDir.Combine("packages");
        context.SourceDir = context.RootDir.Combine("source");
        context.ToolsDir = context.RootDir.Combine("tools");

        // Project information
        context.Title = "Reproducer";
        context.SolutionFile = context.RootDir.CombineWithFilePath("Reproducer.sln");

        // Build information
        var buildSystem = context.BuildSystem();
        context.IsCiBuild = buildSystem.IsRunningOnGitLabCI;
        context.IsMainProjectRepo = buildSystem.GitLabCI.Environment.Project.Path.Equals("dieter/Reproducer", StringComparison.OrdinalIgnoreCase);
        context.IsTagged = !string.IsNullOrWhiteSpace(buildSystem.GitLabCI.Environment.Build.Tag);
        context.IsReleasableBranch = buildSystem.GitLabCI.Environment.Build.RefName.Equals("master", StringComparison.OrdinalIgnoreCase) ||
                                     buildSystem.GitLabCI.Environment.Build.RefName.StartsWith("feature/", StringComparison.OrdinalIgnoreCase) ||
                                     buildSystem.GitLabCI.Environment.Build.RefName.StartsWith("release/", StringComparison.OrdinalIgnoreCase) ||
                                     (context.IsTagged && buildSystem.GitLabCI.Environment.Build.RefName.StartsWith("v", StringComparison.OrdinalIgnoreCase));
        context.IsReleaseBuild = context.Configuration == "Release" &
                                 context.IsCiBuild &&
                                 context.IsMainProjectRepo &&
                                 context.IsReleasableBranch &&
                                 context.IsTagged;
        context.MsBuildDebug = true;
        context.MsBuildBuildLogFile = context.ArtifactsDir.CombineWithFilePath("dotnet_build.binlog");
        context.MsBuildTestLogFile = context.ArtifactsDir.CombineWithFilePath("dotnet_test.binlog");
        context.MsBuildPackLogFile = context.ArtifactsDir.CombineWithFilePath("dotnet_pack.binlog");

        // Configure coverage
        context.CoverageFileName = "coverage.json";
        context.CoverageFile = context.CoverageDir.CombineWithFilePath(context.CoverageFileName);

        // Configure packaging
        //context.NugetPackageFeed = "TODO";
        //context.ShouldPublish = context.IsCiBuild &&
        //                        context.IsMainProjectRepo &&
        //                        context.IsReleasableBranch;
        context.ShouldPublish = false;

        // Increase verbosity for Release builds
        if (context.IsReleasableBranch && (context.Log.Verbosity != Verbosity.Diagnostic))
        {
            context.Information("Increasing verbosity to diagnostic.");
            context.Log.Verbosity = Verbosity.Diagnostic;
        }

        context.Log.Verbosity = Verbosity.Diagnostic;

        // Log useful information
        context.Information(Cake.Figlet.FigletAliases.Figlet(context, context.Title));
    }

    public override void Teardown(Context context, ITeardownContext info)
    {
        context.Information("Finished running tasks.");
    }
}
