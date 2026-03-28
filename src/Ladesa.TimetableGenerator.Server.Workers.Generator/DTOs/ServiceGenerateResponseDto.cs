namespace Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

public record ServiceGenerateResponseDto(
    Guid RequestId,
    bool IsSuccessful,
    ServiceGenerateResponseResultSuccessDto? Success,
    ServiceGenerateResponseResultErrorDto? Error,
    DateOnly DateTimeIssued
);