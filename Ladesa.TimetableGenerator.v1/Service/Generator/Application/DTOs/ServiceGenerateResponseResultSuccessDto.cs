using Ladesa.TimetableGenerator.v1.Core.Domain;

namespace Ladesa.TimetableGenerator.v1.Service.Generator.Application.DTOs;

public record ServiceGenerateResponseResultSuccessDto(
    Guid RequestId,
    GenerateRequest GenerateRequest,
    GeneratedTimetable[] GeneratedTimetables
);