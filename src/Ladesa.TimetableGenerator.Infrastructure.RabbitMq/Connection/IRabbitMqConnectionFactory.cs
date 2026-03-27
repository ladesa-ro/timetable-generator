using RabbitMQ.Client;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;

public interface IRabbitMqConnectionFactory
{
    ConnectionFactory GetConnectionFactory();
}
