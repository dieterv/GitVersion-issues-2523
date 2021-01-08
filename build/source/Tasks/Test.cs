using System.Collections.Generic;

using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core;
using Cake.Coverlet;
using Cake.Frosting;

[TaskName("Test")]
[IsDependentOn(typeof(BuildTask))]
public sealed class TestTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var projects = GlobbingAliases.GetFiles(context, "./source/*.Tests/*.csproj");

        foreach (var project in projects)
        {
            var testSettings = new DotNetCoreTestSettings
            {
                ArgumentCustomization = args => args.Append("/nodeReuse:False").Append("/m"),
                Configuration = context.Configuration,
                Collectors = new List<string>() { "XPlat Code Coverage" },
                Loggers = new List<string>() { "trx", },
                ResultsDirectory = context.TestResultsDir,
            };

            if (context.MsBuildDebug)
            {
                testSettings.ArgumentCustomization = args => args.Append($"/bl:{context.MsBuildTestLogFile}");
            }

            var coverletSettings = new CoverletSettings()
            {
                CoverletOutputDirectory = context.CoverageDir,
                CoverletOutputFormat = CoverletOutputFormat.cobertura,
                Verbosity = Cake.Common.Tools.DotNetCore.DotNetCoreVerbosity.Diagnostic,
            };

            CoverletAliases.DotNetCoreTest(
                context,
                project,
                testSettings,
                coverletSettings);
        }
    }
}
