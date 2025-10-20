namespace Ladesa.TimetableGenerator.v1.Service.Generator.Application.DTOs;

public record ServiceGenerateResponseResultErrorDto(
    string ErrorCode,
    string ErrorMessage,
    string? AdditionalInfo
);