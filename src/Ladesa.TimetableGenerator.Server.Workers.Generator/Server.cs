using Ladesa.TimetableGenerator.Server.Workers.Generator.Extensions.Startup;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

public static class Server
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddTimetableGeneratorInfrastructure()
            .AddGeneratorWorker();

        return services;
    }
}
