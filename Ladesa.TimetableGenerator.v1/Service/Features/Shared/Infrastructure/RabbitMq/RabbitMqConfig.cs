using RabbitMQ.Client;

namespace Ladesa.TimetableGenerator.v1.Service.Features.Shared.Infrastructure.RabbitMq;

public class RabbitMqConfig(IConfiguration config)
{
    public ConnectionFactory GetConnectionFactory()
    {
        var hostName = config["TIMETABLE_SERVICE_BROKER_HOSTNAME"];
        var userName = config["TIMETABLE_SERVICE_BROKER_USERNAME"];
        var password = config["TIMETABLE_SERVICE_BROKER_PASSWORD"];

        if (
            string.IsNullOrEmpty(hostName)
            || string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(password)
        )
            throw new InvalidOperationException(
                "RabbitMqConnectionFactory: TIMETABLE_SERVICE_BROKER_HOSTNAME, TIMETABLE_SERVICE_BROKER_USERNAME or TIMETABLE_SERVICE_BROKER_PASSWORD is missing."
            );

        return new ConnectionFactory
        {
            HostName = hostName,
            UserName = userName,
            Password = password
        };
    }
}