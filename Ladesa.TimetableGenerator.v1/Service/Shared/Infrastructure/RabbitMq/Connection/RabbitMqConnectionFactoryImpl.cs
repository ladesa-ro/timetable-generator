using Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Config;
using RabbitMQ.Client;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Connection;


public class RabbitMqConnectionFactoryImpl(IRabbitMqConfigProvider configProvider)
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