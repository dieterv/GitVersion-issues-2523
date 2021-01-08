using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Core;
using Cake.Frosting;

[TaskName("Build")]
[IsDependentOn(typeof(ValidateTask))]
public sealed class BuildTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var buildSettings = new DotNetCoreBuildSettings
        {
            ArgumentCustomization = args => args.Append("/nodeReuse:False").Append("/m"),
            Configuration = context.Configuration,
            NoRestore = true
        };

        if (context.MsBuildDebug)
        {
            buildSettings.ArgumentCustomization = args => args.Append($"/bl:{context.MsBuildBuildLogFile}");
        }

        // Build solution
        DotNetCoreAliases.DotNetCoreBuild(
            context,
            context.SolutionFile.FullPath,
            buildSettings);
    }
}
