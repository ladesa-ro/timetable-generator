using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

public record ServiceGenerateRequestDto(
    Guid RequestId,
    GenerateTimetableCommand GenerateTimetableCommand
);