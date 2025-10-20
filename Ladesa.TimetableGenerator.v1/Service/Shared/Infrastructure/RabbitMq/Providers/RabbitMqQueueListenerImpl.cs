using Ladesa.TimetableGenerator.v1.Service.Shared.Application.Ports;
using Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Connection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Providers;

/// <summary>
/// Um listener de fila RabbitMQ resiliente que gerencia o seu próprio canal e
/// se reinscreve automaticamente em caso de falha e reconexão.
/// </summary>
public sealed class RabbitMqQueueListenerImpl : IQueueListener, IAsyncDisposable
{
    private readonly RabbitMqPersistentConnectionImpl _persistentConnectionImpl;
    private readonly ILogger<RabbitMqQueueListenerImpl> _logger;
    private readonly SemaphoreSlim _initializationSemaphore = new(1, 1);

    private IChannel? _channel;
    private string? _queueName;
    private string? _consumerTag;
    private Func<byte[], Task>? _messageHandler;
    private bool _disposed;

    public RabbitMqQueueListenerImpl(
        RabbitMqPersistentConnectionImpl persistentConnectionImpl,
        ILogger<RabbitMqQueueListenerImpl> logger)
    {
        _persistentConnectionImpl = persistentConnectionImpl;
        _logger = logger;

        // Assina o evento de reconexão para recriar o consumidor
        _persistentConnectionImpl.OnReconnected += OnConnectionImplReconnected;
    }

    /// <summary>
    /// Inicia o listener e o inscreve na fila especificada.
    /// </summary>
    public async Task SubscribeAsync(
        string queue,
        Func<byte[], Task> handler,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(queue)) throw new ArgumentNullException(nameof(queue));
        _messageHandler = handler ?? throw new ArgumentNullException(nameof(handler));
        _queueName = queue;

        // Se a conexão já existe, inicia o consumidor.
        // Se não, o evento OnReconnected cuidará disso quando a conexão for estabelecida.
        await _persistentConnectionImpl.TryConnectAsync(cancellationToken);
        if (_persistentConnectionImpl.IsConnected) await InitializeConsumerAsync(cancellationToken);
    }

    private async Task InitializeConsumerAsync(CancellationToken cancellationToken = default)
    {
        // Garante que a inicialização não aconteça em paralelo
        await _initializationSemaphore.WaitAsync(cancellationToken);

        try
        {
            if (_channel is { IsOpen: true }) return;

            if (string.IsNullOrEmpty(_queueName) || _messageHandler is null)
            {
                _logger.LogWarning("RabbitMQ Listener: Nome da fila ou handler não configurado. Impossível iniciar.");
                return;
            }

            _logger.LogInformation("Inicializando consumidor para a fila '{QueueName}'...", _queueName);

            _channel = await _persistentConnectionImpl.CreateChannelAsync(cancellationToken);
            _channel.CallbackExceptionAsync += OnChannelCallbackException;

            var dlxName = $"dlx.{_queueName}";
            var dlqName = $"dlq.{_queueName}";

            await _channel.ExchangeDeclareAsync(dlxName, ExchangeType.Fanout,
                cancellationToken: cancellationToken);

            // 2. Declara a fila de dead-letter (onde as mensagens com erro vão parar)
            await _channel.QueueDeclareAsync(dlqName, true, false, false,
                cancellationToken: cancellationToken);

            // 3. Associa (bind) a fila de dead-letter à exchange
            await _channel.QueueBindAsync(dlqName, dlxName, "",
                cancellationToken: cancellationToken);

            // 4. Declara a fila principal com o argumento para usar a DLX
            var args = new Dictionary<string, object> { { "x-dead-letter-exchange", dlxName } };
            await _channel.QueueDeclareAsync(_queueName, true, false, false,
                args, cancellationToken: cancellationToken);

            // Define a qualidade de serviço (quantas mensagens por vez)
            await _channel.BasicQosAsync(0, 5, false, cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += OnMessageReceivedAsync;

            // Inicia o consumo
            _consumerTag = await _channel.BasicConsumeAsync(_queueName, false, consumer, cancellationToken);
            _logger.LogInformation(
                "Consumidor iniciado com sucesso na fila '{QueueName}' com o ConsumerTag '{ConsumerTag}'.", _queueName,
                _consumerTag);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao inicializar o consumidor para a fila '{QueueName}'.", _queueName);
            // Libera o canal se a inicialização falhou
            _channel?.Dispose();
            _channel = null;
        }
        finally
        {
            _initializationSemaphore.Release();
        }
    }

    private async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs ea)
    {
        if (_messageHandler is null) return;

        var message = ea.Body.ToArray();
        try
        {
            await _messageHandler(message);
            // Confirma o recebimento e processamento da mensagem
            await _channel.BasicAckAsync(ea.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar mensagem da fila '{QueueName}'. Enviando para DLQ.", _queueName);
            // Rejeita a mensagem, o que a enviará para a DLX configurada
            await _channel.BasicNackAsync(ea.DeliveryTag, false, false);
        }
    }

    private void OnConnectionImplReconnected()
    {
        if (_disposed) return;
        _logger.LogInformation("Conexão RabbitMQ restabelecida. Tentando reiniciar o consumidor...");
        _ = InitializeConsumerAsync();
    }

    private Task OnChannelCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return Task.CompletedTask;
        _logger.LogWarning(e.Exception, "Exceção no callback do canal. Tentando reiniciar o consumidor...");
        _ = InitializeConsumerAsync();
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        _persistentConnectionImpl.OnReconnected -= OnConnectionImplReconnected;

        try
        {
            if (_channel is not null && _consumerTag is not null)
                // Para de consumir mensagens de forma elegante
                await _channel.BasicCancelAsync(_consumerTag);
            if (_channel is not null)
            {
                _channel.CallbackExceptionAsync -= OnChannelCallbackException;
                _channel.Dispose();
                await _channel.CloseAsync();
            }

            _logger.LogInformation("Listener da fila '{QueueName}' foi descartado com sucesso.", _queueName);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Erro ao descartar o listener da fila '{QueueName}'.", _queueName);
        }
        finally
        {
            _initializationSemaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}