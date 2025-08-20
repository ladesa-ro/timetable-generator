using System.Text;
using System.Text.Json;
using GerarHorarioService.Extensions;
using Ladesa.TimetableGenerator.Core;
using Ladesa.TimetableGenerator.Core.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GerarHorarioService.Workers;

public class ListenWorker(ILogger<ListenWorker> logger, RabbitMqHelpers rabbitMqHelpers)
    : BackgroundService
{
    private IChannel? _channel;
    private IConnection? _connection;
    private ConnectionFactory? _factory;

    private async Task ConfigureQueue(CancellationToken stoppingToken)
    {
        try
        {
            _factory = rabbitMqHelpers.RabbitMqConnectionFactory();
        }
        catch (InvalidOperationException e)
        {
            logger.LogError(e, e.Message);
            await StopAsync(stoppingToken);
        }

        if (_factory is null)
        {
            logger.LogError("RabbitMQ connection factory could not be initialized.");
            return;
        }

        var connection = await _factory.CreateConnectionAsync(stoppingToken);
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        _channel = channel;
        _connection = connection;

        await channel.QueueDeclareAsync(
            "gerar_horario",
            true,
            false,
            false,
            null,
            cancellationToken: stoppingToken
        );

        await channel.QueueDeclareAsync(
            "horario_gerado",
            true,
            false,
            false,
            null,
            cancellationToken: stoppingToken
        );
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConfigureQueue(stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += ListenResponseInGerarHorario;

        await _channel.BasicConsumeAsync(
            "gerar_horario",
            true,
            consumer,
            stoppingToken
        );

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (TaskCanceledException)
        {
            // Ignora exceção ao cancelar a tarefa
        }
    }

    private async Task ListenResponseInGerarHorario(object? model, BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        logger.LogInformation(" [x] Received ");

        var serializationOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(message));

        var gerarHorarioOptions =
            await JsonSerializer.DeserializeAsync<GerarHorarioOptions>(
                stream,
                serializationOptions
            );

        var horarioGerado = Gerador.GerarHorario(gerarHorarioOptions);

        var horarioJson = JsonSerializer.Serialize(horarioGerado);

        await PublishResponseIntoHorarioGerado(horarioJson);
    }

    private async Task PublishResponseIntoHorarioGerado(string horarioJson)
    {
        var body = Encoding.UTF8.GetBytes(horarioJson);

        await _channel.BasicPublishAsync(
            string.Empty,
            "horario_gerado",
            body
        );

        logger.LogInformation($" [x] Horario Gerado {DateTime.Now}");
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Encerrando o consumidor...");
        _channel?.CloseAsync(stoppingToken);
        _connection?.CloseAsync(stoppingToken);
        await base.StopAsync(stoppingToken);
    }
}