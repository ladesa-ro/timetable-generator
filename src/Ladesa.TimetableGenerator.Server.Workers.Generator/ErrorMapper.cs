using System.Text.Json;
using Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

public class ErrorMapper : IErrorMapper
{
    public ServiceGenerateResponseResultErrorDto MapToErrorDto(
        string errorCode,
        string errorMessage,
        Exception ex,
        byte[] originalBytes)
    {
        return new ServiceGenerateResponseResultErrorDto(
            errorCode,
            errorMessage,
            JsonSerializer.Serialize(new { message = ex.Message, bytes = originalBytes })
        );
    }
}
