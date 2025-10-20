namespace Ladesa.TimetableGenerator.v1.Service.Features.Generator.DTOs;

public record ServiceGenerateResponseResultErrorDto(
    string ErrorCode,
    string ErrorMessage,
    string? AdditionalInfo
);