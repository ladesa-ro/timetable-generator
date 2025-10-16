using System.Text;
using System.Text.Json;
using GerarHorarioService.Extensions;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.JsonSerialization;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;
using Ladesa.TimetableGenerator.Features.Gerador;
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

        // Queue for error feedbacks
        await channel.QueueDeclareAsync(
            "horario_erro",
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

        await _channel.BasicConsumeAsync("gerar_horario", false, consumer, stoppingToken);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
            // Ignora exceção ao cancelar a tarefa
        }
    }

    private async Task ListenResponseInGerarHorario(object? model, BasicDeliverEventArgs ea)
    {
        var deliveryTag = ea.DeliveryTag;
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        logger.LogInformation("[x] Received message for timetable generation");

        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(10));
        var token = cts.Token;

        try
        {
            GeradorPayloadDto? payloadDto = null;
            string requestIdStr = string.Empty;
            try
            {
                payloadDto = GeradorPayloadSerializer.ToDto(message);
                requestIdStr = payloadDto.RequestId.ToString();
            }
            catch (Exception ex)
            {
                // Publica também na fila de erro para observabilidade, mas sempre retorna na horario_gerado
                await PublishErrorAsync("parse_error", ex.Message, message);

                var failEnvelope = new
                {
                    request_id = "",
                    sucesso = false,
                    payload = new object(),
                    mensagem = "Falha ao interpretar o payload (parse_error)",
                    contexto = new
                    {
                        type = "parse_error",
                        details = ex.Message,
                        timestamp = DateTimeOffset.UtcNow,
                    },
                };
                await PublishResponseEnvelopeAsync(failEnvelope);

                logger.LogError(ex, "Erro ao fazer parse do payload");
                await _channel.BasicAckAsync(deliveryTag, multiple: false);
                return;
            }

            if (payloadDto is null)
            {
                await PublishErrorAsync(
                    "validation_error",
                    "Payload inválido: GeradorPayloadDto nulo.",
                    message
                );
                var failEnvelope = new
                {
                    request_id = requestIdStr,
                    sucesso = false,
                    payload = new object(),
                    mensagem = "Payload inválido: GeradorPayloadDto nulo.",
                    contexto = new
                    {
                        type = "validation_error",
                        details = "dto nulo",
                        timestamp = DateTimeOffset.UtcNow,
                    },
                };
                await PublishResponseEnvelopeAsync(failEnvelope);
                logger.LogError("Payload inválido: GeradorPayloadDto nulo.");
                await _channel.BasicAckAsync(deliveryTag, multiple: false);
                return;
            }

            var validationErrors = ValidatePayload(payloadDto);
            if (validationErrors.Count > 0)
            {
                await PublishErrorAsync(
                    "validation_error",
                    string.Join("; ", validationErrors),
                    message
                );
                var failEnvelope = new
                {
                    request_id = requestIdStr,
                    sucesso = false,
                    payload = payloadDto,
                    mensagem = "Falha de validação do payload.",
                    contexto = new
                    {
                        type = "validation_error",
                        details = validationErrors,
                        timestamp = DateTimeOffset.UtcNow,
                    },
                };
                await PublishResponseEnvelopeAsync(failEnvelope);
                await _channel.BasicAckAsync(deliveryTag, multiple: false);
                return;
            }

            var processingTask = Task.Run(
                () =>
                {
                    var payload = GeradorPayloadMapper.ToDomain(payloadDto);
                    var horariosGerados = Gerador.GerarHorario(payload);
                    var horariosGeradosDto = horariosGerados
                        .Select(HorarioGeradoMapper.ToDto)
                        .ToArray();
                    return horariosGeradosDto;
                },
                token
            );

            var completed = await Task.WhenAny(
                processingTask,
                Task.Delay(Timeout.InfiniteTimeSpan, token)
            );
            if (completed != processingTask)
            {
                logger.LogWarning(
                    "Tempo limite (10min) excedido na geração de horário. Nack e requeue."
                );
                var failEnvelope = new
                {
                    request_id = requestIdStr,
                    sucesso = false,
                    payload = payloadDto,
                    mensagem = "Tempo limite excedido para geração (timeout)",
                    contexto = new
                    {
                        type = "timeout",
                        details = "10min excedidos",
                        timestamp = DateTimeOffset.UtcNow,
                    },
                };
                await PublishResponseEnvelopeAsync(failEnvelope);

                await _channel.BasicNackAsync(deliveryTag, multiple: false, requeue: true);
                return;
            }

            if (processingTask.IsFaulted)
            {
                var ex =
                    processingTask.Exception?.GetBaseException()
                    ?? new Exception("Erro desconhecido na geração");
                await PublishErrorAsync("generation_error", ex.Message, message);

                var failEnvelope = new
                {
                    request_id = requestIdStr,
                    sucesso = false,
                    payload = payloadDto,
                    mensagem = "Erro ao gerar horário",
                    contexto = new
                    {
                        type = "generation_error",
                        details = ex.Message,
                        timestamp = DateTimeOffset.UtcNow,
                    },
                };
                await PublishResponseEnvelopeAsync(failEnvelope);

                logger.LogError(ex, "Erro ao gerar horário");
                await _channel.BasicAckAsync(deliveryTag, multiple: false);
                return;
            }

            var horariosGeradosDtoResult = await processingTask;
            var successEnvelope = new
            {
                request_id = requestIdStr,
                sucesso = true,
                resultados = horariosGeradosDtoResult,
            };
            await PublishResponseEnvelopeAsync(successEnvelope);
            await _channel.BasicAckAsync(deliveryTag, multiple: false);
        }
        catch (OperationCanceledException)
        {
            var failEnvelope = new
            {
                request_id = "",
                sucesso = false,
                payload = new object(),
                mensagem = "Operação cancelada",
                contexto = new
                {
                    type = "timeout",
                    details = "cancellation token",
                    timestamp = DateTimeOffset.UtcNow,
                },
            };
            await PublishResponseEnvelopeAsync(failEnvelope);
            await _channel.BasicNackAsync(deliveryTag, multiple: false, requeue: true);
        }
        catch (Exception ex)
        {
            await PublishErrorAsync("unexpected_error", ex.Message, message);
            var failEnvelope = new
            {
                request_id = "",
                sucesso = false,
                payload = new object(),
                mensagem = "Erro inesperado ao processar a mensagem",
                contexto = new
                {
                    type = "unexpected_error",
                    details = ex.Message,
                    timestamp = DateTimeOffset.UtcNow,
                },
            };
            await PublishResponseEnvelopeAsync(failEnvelope);
            logger.LogError(ex, "Erro inesperado ao processar mensagem");
            await _channel.BasicAckAsync(deliveryTag, multiple: false);
        }
    }

    private async Task PublishErrorAsync(string type, string details, string originalMessage)
    {
        var payload = new
        {
            type,
            details,
            timestamp = DateTimeOffset.UtcNow,
            original = originalMessage,
        };
        var json = BaseJsonSerializer<object>.ToJson(payload);
        var body = Encoding.UTF8.GetBytes(json);
        await _channel.BasicPublishAsync(string.Empty, "horario_erro", body);
        logger.LogWarning("[x] Horario Erro publicado: {Type}", type);
    }

    private static List<string> ValidatePayload(GeradorPayloadDto dto)
    {
        var errors = new List<string>();
        return errors;
    }

    private async Task PublishResponseEnvelopeAsync(object envelope)
    {
        var json = BaseJsonSerializer<object>.ToJson(envelope);
        await PublishResponseIntoHorarioGerado(json);
    }

    private async Task PublishResponseIntoHorarioGerado(string horarioJson)
    {
        var body = Encoding.UTF8.GetBytes(horarioJson);

        await _channel.BasicPublishAsync(string.Empty, "horario_gerado", body);

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
