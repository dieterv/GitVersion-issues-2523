using Cake.Common.IO;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

[TaskName("Report")]
[IsDependentOn(typeof(TestTask))]
public sealed class ReportTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        ReportGeneratorAliases.ReportGenerator(
            context,
            new GlobPattern($"{context.TestResultsDir.FullPath}/*-*-*-*-*/*.cobertura.xml"),
            context.CoverageDir,
            new ReportGeneratorSettings()
            {
                ReportTypes = new ReportGeneratorReportType[] { ReportGeneratorReportType.Cobertura, },
            });
    }
}
