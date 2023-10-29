using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Planner.BusinessLogic;
using Planner.BusinessLogic.Interfaces;
using Planner.Data;
using Planner.Infrastructure;

namespace PlannerApp;

public class Program
{
    public static void Main(string[] args)
    {
        //setup our DI
        var serviceProvider = new ServiceCollection()
            .AddLogging(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                .AddConsole()
                .AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.SingleLine = true;
                    options.TimestampFormat = "HH:mm:ss ";
                })
                .AddProvider(new CustomFileLoggerProvider())
            )
            .AddSingleton<PlannerBase>()
            .AddSingleton<ITaskService, TaskService>()
            .AddSingleton<IStorage>(_ => new Storage("tasks.json"))
            .BuildServiceProvider();

        var logger = serviceProvider.GetService<ILoggerFactory>()?
            .CreateLogger<Program>();
        logger.LogDebug("Starting application");

        var bar = serviceProvider.GetService<PlannerBase>();
        bar.Start();

        logger.LogDebug("All done!");

    }
}