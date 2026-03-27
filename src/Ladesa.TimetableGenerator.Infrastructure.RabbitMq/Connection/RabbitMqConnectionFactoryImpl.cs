using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Config;
using RabbitMQ.Client;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;


public class RabbitMqConnectionFactoryImpl(IRabbitMqConfigProvider configProvider) : IRabbitMqConnectionFactory
{
    public ConnectionFactory GetConnectionFactory()
    {
        var connectionOptions = configProvider.GetConnectionOptions();
        
        return new ConnectionFactory
        {
            HostName = connectionOptions.HostName,
            UserName = connectionOptions.UserName,
            Password = connectionOptions.Password
        };
    }
}