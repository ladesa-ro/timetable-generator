namespace Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

public record ServiceGenerateResponseResultErrorDto(
    string ErrorCode,
    string ErrorMessage,
    string? AdditionalInfo
);