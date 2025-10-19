using Ladesa.TimetableGenerator.v1.Core.Application.DTOs;
using Ladesa.TimetableGenerator.v1.Core.Domain.Entities;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.DTOs;

public record ServiceGenerateResponseResultSuccessDto(
    Guid RequestId,
    GenerateRequest GenerateRequest,
    GeneratedTimetable[] GeneratedTimetables
);