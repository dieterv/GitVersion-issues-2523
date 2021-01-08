using Cake.Frosting;

[TaskName("Default")]
[IsDependentOn(typeof(PublishTask))]
public sealed class DefaultTask : FrostingTask<Context>
{
}
