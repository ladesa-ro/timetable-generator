using GerarHorarioService.Extensions;
using RabbitMQ.Client;

namespace Ladesa.TimetableGenerator.Service.Workers;

public class ListenWorker(ILogger<ListenWorker> logger, RabbitMqHelpers rabbitMqHelpers)
    : BackgroundService
{
    private IChannel? _channel;
    private IConnection? _connection;
    private ConnectionFactory? _factory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        await base.StopAsync(stoppingToken);
    }
}