using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

public class GenerateResponseBuilder(ISystemClock systemClock)
{
    public ServiceGenerateResponseDto BuildSuccess(
        Guid requestId,
        GenerateTimetableCommand generateTimetableCommand,
        GenerateTimetableCommandResponse[] generatedTimetables)
    {
        var successDto = new ServiceGenerateResponseResultSuccessDto(
            requestId,
            generateTimetableCommand,
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
