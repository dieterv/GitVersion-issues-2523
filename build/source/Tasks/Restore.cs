using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;

[TaskName("Restore")]
[IsDependentOn(typeof(CleanTask))]
public sealed class RestoreTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetCoreRestore(context.SolutionFile.FullPath);
    }
}
