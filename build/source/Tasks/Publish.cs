using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Core;
using Cake.Frosting;

[TaskName("Publish")]
[IsDependentOn(typeof(PackageTask))]
public sealed class PublishTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        // Copy packages to build/artifacts/packages
        var packages = GlobbingAliases.GetFiles(context, "./source/**/*.symbols.nupkg");

        foreach (var package in packages)
        {
            context.CopyFileToDirectory(package, context.PackagesDir);
        }

        // Publish packages
        if (context.ShouldPublish)
        {
            foreach (var package in packages)
            {
                DotNetCoreAliases.DotNetCoreNuGetPush(
                    context,
                    package.FullPath,
                    new DotNetCoreNuGetPushSettings()
                    {
                        ArgumentCustomization = args => args.Append("/nodeReuse:False"),
                        Source = "https://www.example.com/nugetfeed",
                        ApiKey = "4003d786-cc37-4004-bfdf-c4f3e8ef9b3a",
                    });
            }
        }
        else
        {
            context.Information("Publishing skipped");
        }
    }
}
