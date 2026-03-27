using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Config;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Config;

public class GeneratorListerWorkerConfigEnvironmentImpl(IConfiguration config) : IGeneratorListenWorkerConfig
{
    private const string EnvQueueListen = "TIMETABLE_SERVICE_BROKER_QUEUE_LISTEN";
    private const string EnvQueueReply = "TIMETABLE_SERVICE_BROKER_QUEUE_REPLY";

    public IGeneratorListenWorkerConfig.GeneratorListenWorkerConfig GetConfig()
    {
        return new IGeneratorListenWorkerConfig.GeneratorListenWorkerConfig(
            QueueListen: config.GetRequiredValue(EnvQueueListen),
            QueueReply: config.GetRequiredValue(EnvQueueReply)
        );
    }
}
