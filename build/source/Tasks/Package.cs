using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;
using Cake.Frosting;

[TaskName("Package")]
[IsDependentOn(typeof(ReportTask))]
public sealed class PackageTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var packSettings = new DotNetCorePackSettings()
        {
            ArgumentCustomization = args => args.Append("/nodeReuse:False").Append("/m"),
            Configuration = context.Configuration,
            IncludeSymbols = true,
            NoBuild = true,
        };

        if (context.MsBuildDebug)
        {
            packSettings.ArgumentCustomization = args => args.Append($"/bl:{context.MsBuildPackLogFile}");
        }

        DotNetCoreAliases.DotNetCorePack(
            context,
            context.SolutionFile.FullPath,
            packSettings);
    }
}
