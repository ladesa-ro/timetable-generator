using System.Text.Json;
using Ladesa.TimetableGenerator.Application.Abstractions;
using Ladesa.TimetableGenerator.Application.Todo.Generator;
using Ladesa.TimetableGenerator.Application.Todo.Generator.DTOs;
using Ladesa.TimetableGenerator.Application.Todo.Ports;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;
using Ladesa.TimetableGenerator.Server.Workers.Generator.Config;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

public class GeneratorListenWorker(
    IGeneratorListenWorkerConfig generatorListenWorkerConfig,
    IQueueListener queueListener,
    IQueuePublisher queuePublisher,
    ITimetableGeneratorService timetableGeneratorService,
    GenerateResponseBuilder responseBuilder,
    IMessageDeserializer<ServiceGenerateRequestDto> requestDeserializer,
    IMessageSerializer<ServiceGenerateResponseDto> responseSerializer,
    IErrorMapper errorMapper,
    ILogger<GeneratorListenWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = generatorListenWorkerConfig.GetConfig();
        await queueListener.SubscribeAsync(
            config.QueueListen,
            async bytes =>
            {
                var requestDto = DeserializeRequest(bytes);
                await GenerateAndPublish(requestDto, config.QueueReply, bytes, stoppingToken);
            },
            stoppingToken
        );
    }

    private ServiceGenerateRequestDto DeserializeRequest(byte[] bytes)
    {
        try
        {
            return requestDeserializer.Deserialize(bytes);
        }
        catch (JsonException ex)
        {
            logger.LogWarning(ex, "Failed to deserialize timetableCommand message: invalid JSON ({ByteLength} bytes).", bytes.Length);
            var errorDto = errorMapper.MapToErrorDto(GeneratorErrorCodes.ParseError, GeneratorErrorMessages.ParseError, ex, bytes);
            // TODO: currently goes to dead letter; consider pulling timetableCommand-id from a header
            throw new Exception(JsonSerializer.Serialize(errorDto));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error deserializing timetableCommand message ({ByteLength} bytes).", bytes.Length);
            var errorDto = errorMapper.MapToErrorDto(GeneratorErrorCodes.ParseError, GeneratorErrorMessages.ParseError, ex, bytes);
            throw new Exception(JsonSerializer.Serialize(errorDto));
        }
    }

    private async Task GenerateAndPublish(
        ServiceGenerateRequestDto requestDto,
        string replyQueue,
        byte[] originalBytes,
        CancellationToken stoppingToken)
    {
        try
        {
            var generatedTimetables = timetableGeneratorService
                .Generate(requestDto.GenerateTimetableCommand)
                .Take(1)
                .ToArray();

            var responseDto = responseBuilder.BuildSuccess(
                requestDto.RequestId, requestDto.GenerateTimetableCommand, generatedTimetables);

            await PublishResponse(responseDto, replyQueue, stoppingToken);
        }
        catch (Exception ex)
        {
            var logLevel = ex is GeneratorValidationException ? LogLevel.Warning : LogLevel.Error;
            logger.Log(logLevel, ex, "Error during timetable generation for timetableCommand '{RequestId}'.", requestDto.RequestId);

            var errorDto = errorMapper.MapToErrorDto(
                GeneratorErrorCodes.GenerationError, GeneratorErrorMessages.GenerationError, ex, originalBytes);

            var responseDto = responseBuilder.BuildError(requestDto.RequestId, errorDto);
            await PublishResponse(responseDto, replyQueue, stoppingToken);
        }
    }

    private async Task PublishResponse(ServiceGenerateResponseDto responseDto, string replyQueue, CancellationToken stoppingToken)
    {
        var bytes = responseSerializer.Serialize(responseDto);
        await queuePublisher.PublishAsync(replyQueue, bytes, stoppingToken);
    }
}
