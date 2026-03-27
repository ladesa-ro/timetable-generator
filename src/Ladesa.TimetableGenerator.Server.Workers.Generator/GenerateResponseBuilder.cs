using Ladesa.TimetableGenerator.Application.Generator.DTOs;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

public class GenerateResponseBuilder(ISystemClock systemClock)
{
    public ServiceGenerateResponseDto BuildSuccess(
        Guid requestId,
        GenerateRequest generateRequest,
        GeneratedTimetable[] generatedTimetables)
    {
        var successDto = new ServiceGenerateResponseResultSuccessDto(
            requestId,
            generateRequest,
            generatedTimetables
        );

        return new ServiceGenerateResponseDto(
            requestId,
            true,
            successDto,
            null,
            systemClock.Today
        );
    }

    public ServiceGenerateResponseDto BuildError(
        Guid requestId,
        ServiceGenerateResponseResultErrorDto errorDto)
    {
        return new ServiceGenerateResponseDto(
            requestId,
            false,
            null,
            errorDto,
            systemClock.Today
        );
    }
}
