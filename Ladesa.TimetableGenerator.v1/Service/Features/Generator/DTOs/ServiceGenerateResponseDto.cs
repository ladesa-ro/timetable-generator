namespace Ladesa.TimetableGenerator.v1.Service.Features.Generator.DTOs;

public record ServiceGenerateResponseDto(
    Guid RequestId,
    bool IsSuccessful,
    ServiceGenerateResponseResultSuccessDto? Success,
    ServiceGenerateResponseResultErrorDto? Error,
    DateOnly DateTimeIssued
);