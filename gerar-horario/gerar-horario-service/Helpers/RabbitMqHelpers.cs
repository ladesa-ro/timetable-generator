using RabbitMQ.Client;

namespace GerarHorarioService.Extensions;

public class RabbitMqHelpers
{
    IConfiguration _configuration;

    public RabbitMqHelpers(IConfiguration config)
    {
        _configuration = config;
    }

    public ConnectionFactory RabbitMqConnectionFactory()
    {
        var hostName = _configuration["HostName"];
        var userName = _configuration["RabbitMQUserName"];
        var password = _configuration["Password"];

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

        return new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
        };
    }
}
