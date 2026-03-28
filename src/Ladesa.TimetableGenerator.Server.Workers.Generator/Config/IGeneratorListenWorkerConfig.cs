namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Config;

public record GeneratorListenWorkerConfig(
    string QueueListen,
    string QueueReply
);

public interface IGeneratorListenWorkerConfig
{
    GeneratorListenWorkerConfig GetConfig();
}
