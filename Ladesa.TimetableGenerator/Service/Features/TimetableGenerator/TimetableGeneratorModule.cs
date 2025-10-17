using GerarHorarioService.Extensions;
using Ladesa.TimetableGenerator.Service.Workers;

namespace Ladesa.TimetableGenerator.Service.Features.Health;

public static class TimetableGeneratorModule
{
    public static IServiceCollection AddTimetableGeneratorModule(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqHelpers>();
        services.AddHostedService<ListenWorker>();

        return services;
    }
}