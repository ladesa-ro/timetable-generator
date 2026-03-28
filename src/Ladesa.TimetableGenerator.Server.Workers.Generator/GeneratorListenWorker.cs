using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Server.Workers.Generator.Config;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

public class GeneratorListenWorker(
    IGeneratorListenWorkerConfig generatorListenWorkerConfig,
    IQueueListener queueListener,
    IQueuePublisher queuePublisher,
    GenerationRequestProcessor processor,
    ILogger<GeneratorListenWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = generatorListenWorkerConfig.GetConfig();
        await queueListener.SubscribeAsync(
            config.QueueListen,
            async bytes =>
            {
                var responseBytes = processor.Process(bytes);
                await queuePublisher.PublishAsync(config.QueueReply, responseBytes, stoppingToken);
            },
            stoppingToken
        );
    }
}
