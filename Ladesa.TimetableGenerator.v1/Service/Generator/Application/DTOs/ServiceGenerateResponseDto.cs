namespace Ladesa.TimetableGenerator.v1.Service.Generator.Application.DTOs;

public record ServiceGenerateResponseDto(
    Guid RequestId,
    bool IsSuccessful,
    ServiceGenerateResponseResultSuccessDto? Success,
    ServiceGenerateResponseResultErrorDto? Error,
    DateOnly DateTimeIssued
);