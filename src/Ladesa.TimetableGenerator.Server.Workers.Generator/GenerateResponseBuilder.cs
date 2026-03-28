using Ladesa.TimetableGenerator.Application.Abstractions;
using Ladesa.TimetableGenerator.Application.Todo.Generator.DTOs;
using Ladesa.TimetableGenerator.Domain.Commands;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;
using Ladesa.TimetableGenerator.Domain.Generator.GenerateRequest;
using Ladesa.TimetableGenerator.Domain.Models;

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
