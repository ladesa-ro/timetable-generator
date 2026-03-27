namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Config;

public interface IRabbitMqConfigProvider
{
    public RabbitMqConfig GetConnectionOptions();

    public record RabbitMqConfig(
        string HostName,
        string UserName,
        string Password,
        ushort PrefetchCount = 5
    );
}