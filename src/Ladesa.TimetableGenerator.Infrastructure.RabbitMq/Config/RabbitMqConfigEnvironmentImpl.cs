using Ladesa.TimetableGenerator.Application.Extensions;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Config;

public class RabbitMqConfigEnvironmentImpl(IConfiguration config) : IRabbitMqConfigProvider
{
    private const string EnvHostname = "TIMETABLE_SERVICE_BROKER_HOSTNAME";
    private const string EnvUsername = "TIMETABLE_SERVICE_BROKER_USERNAME";
    private const string EnvPassword = "TIMETABLE_SERVICE_BROKER_PASSWORD";
    private const string EnvPrefetchCount = "TIMETABLE_SERVICE_BROKER_PREFETCH_COUNT";

    public IRabbitMqConfigProvider.RabbitMqConfig GetConnectionOptions()
    {
        var prefetchCount = ushort.TryParse(config[EnvPrefetchCount], out var parsed) ? parsed : (ushort)5;

        return new IRabbitMqConfigProvider.RabbitMqConfig(
            HostName: config.GetRequiredValue(EnvHostname),
            UserName: config.GetRequiredValue(EnvUsername),
            Password: config.GetRequiredValue(EnvPassword),
            PrefetchCount: prefetchCount
        );
    }
}
