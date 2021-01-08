using Cake.Frosting;

[TaskName("Validate")]
[IsDependentOn(typeof(RestoreTask))]
public sealed class ValidateTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        // Nothing to validate yet...
    }
}
