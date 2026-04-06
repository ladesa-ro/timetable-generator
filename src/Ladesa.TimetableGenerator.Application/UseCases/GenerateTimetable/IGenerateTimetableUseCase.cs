namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

public interface IGenerateTimetableUseCase
{
    Task<GenerateTimetableCommandResponse> HandleAsync(GenerateTimetableCommand command);
}
