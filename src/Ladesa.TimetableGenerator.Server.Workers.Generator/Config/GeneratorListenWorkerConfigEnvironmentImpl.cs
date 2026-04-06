using Ladesa.TimetableGenerator.Application.Abstractions.Configuration.Extensions;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Config;

public class GeneratorListenWorkerConfigEnvironmentImpl(IConfiguration config) : IGeneratorListenWorkerConfig
{
    private const string EnvQueueListen = "TIMETABLE_SERVICE_BROKER_QUEUE_LISTEN";
    private const string EnvQueueReply = "TIMETABLE_SERVICE_BROKER_QUEUE_REPLY";

    public GeneratorListenWorkerConfig GetConfig()
    {
        return new GeneratorListenWorkerConfig(
            QueueListen: config.GetRequiredValue(EnvQueueListen),
            QueueReply: config.GetRequiredValue(EnvQueueReply)
        );
    }
}
