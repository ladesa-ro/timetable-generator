using Ladesa.TimetableGenerator.v1.Core.Domain;

namespace Ladesa.TimetableGenerator.v1.Service.Generator.Application.DTOs;

public record ServiceGenerateRequestDto(
    Guid RequestId,
    GenerateRequest GenerateRequest
);