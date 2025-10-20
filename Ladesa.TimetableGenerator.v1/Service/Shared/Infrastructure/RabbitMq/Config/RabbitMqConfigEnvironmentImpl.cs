namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Config;

public class RabbitMqConfigEnvironmentImpl (IConfiguration config): IRabbitMqConfigProvider
{
    private const string EnvHostname = "TIMETABLE_SERVICE_BROKER_HOSTNAME";
    private const string EnvUsername = "TIMETABLE_SERVICE_BROKER_USERNAME";
    private const string EnvPassword = "TIMETABLE_SERVICE_BROKER_PASSWORD";

    public IRabbitMqConfigProvider.RabbitMqConfig GetConnectionOptions()
    {
        var hostName = config[EnvHostname];
        var userName = config[EnvUsername];
        var password = config[EnvPassword];

        if (
            string.IsNullOrEmpty(hostName)
            || string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(password)
        )
        {
            throw new InvalidOperationException(
                $"GetConnectionOptions: {EnvHostname}, {EnvUsername} or {EnvPassword} is missing."
            );
        }

        return new IRabbitMqConfigProvider.RabbitMqConfig(
            HostName: hostName,
            UserName: userName,
            Password: password
        );
    }
}