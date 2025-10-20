using Ladesa.TimetableGenerator.v1.Service.Shared.Application.Ports;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq;

public static class RabbitMqModule
{
    public static IServiceCollection AddModuleInfrastructureRabbitMq(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqConfig>();
        services.AddSingleton<RabbitMqPersistentConnection>();

        services.AddSingleton<IQueueListener, RabbitMqQueueListener>();
        services.AddSingleton<IQueuePublisher, RabbitMqQueuePublisher>();
        services.AddSingleton<IDeadLetterHandler, RabbitMqDeadLetterHandler>();

        return services;
    }
}