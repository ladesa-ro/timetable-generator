using RabbitMQ.Client;

namespace GerarHorarioService.Extensions;

public class RabbitMqHelpers
{
    private readonly IConfiguration _configuration;

    public RabbitMqHelpers(IConfiguration config)
    {
        _configuration = config;
    }

    public ConnectionFactory RabbitMqConnectionFactory()
    {
        var hostName = _configuration["TIMETABLE_SERVICE_BROKER_HOSTNAME"];
        var userName = _configuration["TIMETABLE_SERVICE_BROKER_USERNAME"];
        var password = _configuration["TIMETABLE_SERVICE_BROKER_PASSWORD"];

        Console.WriteLine(hostName);
        Console.WriteLine(userName);
        Console.WriteLine(password);

        if (
            string.IsNullOrEmpty(hostName)
            || string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(password)
        )
        {
            throw new InvalidOperationException("HostName or UserName is missing.");
        }

        return new ConnectionFactory
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
        };
    }
}
