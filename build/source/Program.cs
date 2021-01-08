using System;

using Cake.Core;
using Cake.Frosting;

public class Program
{
    public static int Main(string[] args)
    {
        // Create and run the host.
        return new CakeHost()
            .UseWorkingDirectory("../..")
            .UseContext<Context>()
            .UseLifetime<Lifetime>()
            .UseTool(new Uri("nuget:?package=ReportGenerator&version=4.8.1"))
            .Run(args);
    }
}
