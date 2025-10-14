using System.Text;
using GerarHorarioService.Extensions;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation;
using Ladesa.TimetableGenerator.Features.Gerador;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GerarHorarioService.Workers;

public class ListenWorker(ILogger<ListenWorker> logger, RabbitMqHelpers rabbitMqHelpers)
    : BackgroundService
{
    private IChannel? Channel;
    private IConnection? Connection;
    private ConnectionFactory? Factory;

    private async Task ConfigureQueue(CancellationToken stoppingToken)
    {
        try
        {
            Factory = rabbitMqHelpers.RabbitMqConnectionFactory();
        }
        catch (InvalidOperationException e)
        {
            logger.LogError(e, e.Message);
            await StopAsync(stoppingToken);
        }

        if (Factory is null)
        {
            logger.LogError("RabbitMQ connection factory could not be initialized.");
            return;
        }

        var connection = await Factory.CreateConnectionAsync(stoppingToken);
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        Channel = channel;
        Connection = connection;

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

        var consumer = new AsyncEventingBasicConsumer(Channel);

        consumer.ReceivedAsync += ListenResponseInGerarHorario;

        await Channel.BasicConsumeAsync("gerar_horario", true, consumer, stoppingToken);

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

        var payloadDto = TimetableJson.ParseGeradorPayload(message);

        if (payloadDto is null)
        {
            logger.LogError("Payload inválido: GeradorPayloadDto nulo.");
            return;
        }

        var payload = GeradorPayloadMapper.ToDomain(payloadDto);

        var horariosGerados = Gerador.GerarHorario(payload);

        var horariosGeradosDto = horariosGerados.Select(HorarioGeradoMapper.ToDto).ToArray();

        var horarioJson = TimetableJson.Stringify(horariosGeradosDto);

        await PublishResponseIntoHorarioGerado(horarioJson);
    }

    private async Task PublishResponseIntoHorarioGerado(string horarioJson)
    {
        var body = Encoding.UTF8.GetBytes(horarioJson);

        await Channel.BasicPublishAsync(string.Empty, "horario_gerado", body);

        logger.LogInformation($" [x] Horario Gerado {DateTime.Now}");
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Encerrando o consumidor...");
        Channel?.CloseAsync(stoppingToken);
        Connection?.CloseAsync(stoppingToken);
        await base.StopAsync(stoppingToken);
    }
}
