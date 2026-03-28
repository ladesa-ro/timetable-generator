using Ladesa.TimetableGenerator.Application.Abstractions;
using Ladesa.TimetableGenerator.Application.Generator.Services;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;

namespace Ladesa.TimetableGenerator.Application.Commands.GenerateTimetableCommand;
public class GenerateTimetableCommandHandler(IGenerateTimetablesHandler handler) : IGenerateTimetableCommandHandler
{
    private IGenerateTimetablesHandler _handler = handler ?? throw new ArgumentNullException("");
    
    public Task<GenerateTimetableCommandResponse> HandleAsync(Domain.Commands.GenerateTimetableCommand.GenerateTimetableCommand command)
    {
        ValidateCommand(command);

        var responses = this._handler.Handle(command);
        
        var first = responses.First();

        return Task.FromResult(first);
    }

    private static void ValidateCommand(Domain.Commands.GenerateTimetableCommand.GenerateTimetableCommand command)
    {
        // Validate time slots: must be strictly increasing within the day (no zero-length, no spanning midnight)
        foreach (var slot in command.TimeSlots)
        {
            var start = TimeSpan.Parse(slot.Start);
            var end = TimeSpan.Parse(slot.End);
            if (start >= end)
                throw new ArgumentException("Invalid time slot: start must be before end within the same day.");
        }
        
        GeneratorValidationException.ValidateNoDuplicates(command.Groups, g => g.Id,
            GeneratorValidationErrorCode.DuplicateGroupId, "Groups");
        
        GeneratorValidationException.ValidateNoDuplicates(command.Teachers, t => t.Id,
            GeneratorValidationErrorCode.DuplicateTeacherId, "Teachers");
        
        GeneratorValidationException.ValidateNoDuplicates(command.Diaries, d => d.Id,
            GeneratorValidationErrorCode.DuplicateDiaryId, "Diaries");
    }
}
