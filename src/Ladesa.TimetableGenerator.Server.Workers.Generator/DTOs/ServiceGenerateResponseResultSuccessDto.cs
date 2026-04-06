using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

public record ServiceGenerateResponseResultSuccessDto(
    Guid RequestId,
    GenerateTimetableCommand GenerateTimetableCommand,
    GenerateTimetableCommandResponse[] GeneratedTimetables
);