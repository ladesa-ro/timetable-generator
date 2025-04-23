using GerarHorarioService.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Sisgea.GerarHorario.Core.Dtos.Configuracoes;
using Sisgea.GerarHorario.Core.Dtos.Entidades;
using Sisgea.GerarHorario.Core.Dtos.HorarioGerado;
using Sisgea.GerarHorario.Core;

namespace GerarHorarioService.Workers;

public class ListenWorker(ILogger<ListenWorker> logger, RabbitMqHelpers rabbitMqHelpers) : BackgroundService
{
    private IConnection? _connection = null;
    private IChannel? _channel = null;
    private ConnectionFactory? _factory = null;
    

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

        await channel.QueueDeclareAsync(queue: "gerar_horario",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: "horario_gerado",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken
        );
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConfigureQueue(stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);


        consumer.ReceivedAsync += ListenResponseInGerarHorario;


        await _channel.BasicConsumeAsync(queue: "gerar_horario",
            autoAck: true,
            consumer: consumer, cancellationToken: stoppingToken);

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


        logger.LogInformation($" [x] Received ");

        var serializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(message));

        GerarHorarioOptions? gerarHorarioOptions = await
            JsonSerializer.DeserializeAsync<GerarHorarioOptions>(stream, serializationOptions);
        

        var horarioGerado = Gerador.GerarHorario(gerarHorarioOptions);

        var horarioJson = JsonSerializer.Serialize(horarioGerado);

        await PublishResponseIntoHorarioGerado(horarioJson);
    }

    private async Task PublishResponseIntoHorarioGerado(string horarioJson)
    {
        var body = Encoding.UTF8.GetBytes(horarioJson);


        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: "horario_gerado", body: body);

        logger.LogInformation($" [x] Horario Gerado {DateTime.Now}");
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Encerrando o consumidor...");
        _channel?.CloseAsync(cancellationToken: stoppingToken);
        _connection?.CloseAsync(cancellationToken: stoppingToken);
        await base.StopAsync(stoppingToken);
    }
}
    
   
     

    