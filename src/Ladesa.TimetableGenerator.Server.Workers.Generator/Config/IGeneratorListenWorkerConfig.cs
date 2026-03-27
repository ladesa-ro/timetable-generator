namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Config;

public interface IGeneratorListenWorkerConfig
{
    public GeneratorListenWorkerConfig GetConfig();
    
    public record GeneratorListenWorkerConfig(
         string QueueListen,
         string QueueReply
    );
}
