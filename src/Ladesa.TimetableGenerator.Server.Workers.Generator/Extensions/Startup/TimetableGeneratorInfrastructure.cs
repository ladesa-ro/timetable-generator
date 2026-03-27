using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Config;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Providers;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Extensions.Startup;

public static class TimetableGeneratorInfrastructureExtensions
{
    public static IServiceCollection AddTimetableGeneratorInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IRabbitMqConfigProvider, RabbitMqConfigEnvironmentImpl>();
        services.AddSingleton<IRabbitMqConnectionFactory, RabbitMqConnectionFactoryImpl>();
        services.AddSingleton<IRabbitMqPersistentConnection, RabbitMqPersistentConnectionImpl>();
        services.AddSingleton<IQueueListener, RabbitMqQueueListenerImpl>();
        services.AddSingleton<IQueuePublisher, RabbitMqQueuePublisherImpl>();
        services.AddSingleton<IDeadLetterHandler, RabbitMqDeadLetterHandlerImpl>();

        return services;
    }
}
