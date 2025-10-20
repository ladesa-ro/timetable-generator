namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Config;

public interface IRabbitMqConfigProvider
{
    public RabbitMqConfig GetConnectionOptions();
    
    public record RabbitMqConfig(
        string HostName,
        string UserName,
        string Password
    );
}