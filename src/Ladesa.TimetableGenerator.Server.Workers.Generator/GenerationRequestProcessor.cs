using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Exceptions;
using Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

public class GenerationRequestProcessor(
    IGenerateTimetableUseCase generateTimetableUseCase,
    IMessageDeserializer<ServiceGenerateRequestDto> requestDeserializer,
    IMessageSerializer<ServiceGenerateResponseDto> responseSerializer,
    GenerateResponseBuilder responseBuilder,
    IErrorMapper errorMapper,
    ILogger<GenerationRequestProcessor> logger)
{
    public byte[] Process(byte[] requestBytes)
    {
        ServiceGenerateRequestDto? requestDto = null;

        try
        {
            requestDto = requestDeserializer.Deserialize(requestBytes);

            var result = generateTimetableUseCase.HandleAsync(requestDto.GenerateTimetableCommand).GetAwaiter().GetResult();

            var responseDto = responseBuilder.BuildSuccess(
                requestDto.RequestId, requestDto.GenerateTimetableCommand, [result]);

            return responseSerializer.Serialize(responseDto);
        }
        catch (Exception ex)
        {
            var (errorCode, errorMessage) = ex switch
            {
                System.Text.Json.JsonException => (GeneratorErrorCodes.ParseError, GeneratorErrorMessages.ParseError),
                GeneratorValidationException => (GeneratorErrorCodes.GenerationError, GeneratorErrorMessages.GenerationError),
                _ => (GeneratorErrorCodes.GenerationError, GeneratorErrorMessages.GenerationError)
            };

            var logLevel = ex is GeneratorValidationException ? LogLevel.Warning : LogLevel.Error;
            logger.Log(logLevel, ex, "Error processing timetable generation request.");

            var errorDto = errorMapper.MapToErrorDto(errorCode, errorMessage, ex, requestBytes);
            var responseDto = responseBuilder.BuildError(
                requestDto?.RequestId ?? Guid.Empty, errorDto);

            return responseSerializer.Serialize(responseDto);
        }
    }
}
