namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;

public interface IGenerateTimetableCommandHandler
{
    public Task<GenerateTimetableCommandResponse> HandleAsync(GenerateTimetableCommand command);
}
