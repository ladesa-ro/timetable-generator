namespace Ladesa.TimetableGenerator.v1.Service.Features.Generator.Config;

public class GeneratorListerWorkerConfigEnvironmentImpl (IConfiguration config): IGeneratorListenWorkerConfig
{
    private const string QueueListen = "TIMETABLE_BROKER_QUEUE_LISTEN";
    private const string QueueReply = "TIMETABLE_BROKER_QUEUE_REPLY";

    public IGeneratorListenWorkerConfig.GeneratorListenWorkerConfig GetConfig()
    {
        var queueListen = config[QueueListen];
        var queueReply = config[QueueReply];

        if (
            string.IsNullOrEmpty(queueListen)
            || string.IsNullOrEmpty(queueReply)
        )
        {
            throw new InvalidOperationException(
                $"GetConfig: {QueueListen} or {QueueReply} is missing."
            );
        }

        return new IGeneratorListenWorkerConfig.GeneratorListenWorkerConfig(
            QueueListen: queueListen,
            QueueReply: queueReply
        );
    }
}