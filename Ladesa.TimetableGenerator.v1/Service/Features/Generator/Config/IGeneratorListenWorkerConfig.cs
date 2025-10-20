namespace Ladesa.TimetableGenerator.v1.Service.Features.Generator.Config;

public interface IGeneratorListenWorkerConfig
{
    public GeneratorListenWorkerConfig GetConfig();
    
    public record GeneratorListenWorkerConfig(
         string QueueListen,
         string QueueReply
    );
}
