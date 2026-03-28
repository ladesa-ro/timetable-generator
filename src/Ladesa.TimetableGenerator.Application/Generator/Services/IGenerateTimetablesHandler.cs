using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;

namespace Ladesa.TimetableGenerator.Application.Generator.Services;

public interface IGenerateTimetablesHandler
{
    IEnumerable<GenerateTimetableCommandResponse> Handle(GenerateTimetableCommand timetableCommand);
}
