using Ladesa.TimetableGenerator.v1.Core.Application.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.DTOs;

public record ServiceGenerateRequestDto(
    Guid RequestId,
    GenerateRequest GenerateRequest
);