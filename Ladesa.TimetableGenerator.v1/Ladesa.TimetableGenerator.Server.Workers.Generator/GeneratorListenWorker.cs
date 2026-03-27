using System.Text;
using System.Text.Json;
using Ladesa.TimetableGenerator.Server.Workers.Generator.Config;
using Ladesa.TimetableGenerator.Application.Generator;
using Ladesa.TimetableGenerator.Application.Generator.DTOs;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Application.Generator.Mappers;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

public class GeneratorListenWorker(
    IGeneratorListenWorkerConfig generatorListenWorkerConfig,
    IQueueListener queueListener,
    IQueuePublisher queuePublisher,
    ITimetableGeneratorService timetableGeneratorService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = generatorListenWorkerConfig.GetConfig();
        await queueListener.SubscribeAsync(
            config.QueueListen,
            async bytes =>
            {
                var parsedRequest = ParseRequest(bytes);
                var mappedRequest = MapRequest(parsedRequest, bytes);
                await GenerateAndPublish(mappedRequest, config.QueueReply, bytes, stoppingToken);
            },
            stoppingToken
        );
    }

    private static Msg.GenerateRequest ParseRequest(byte[] bytes)
    {
        try
        {
            var json = Encoding.UTF8.GetString(bytes);
            return Msg.GenerateRequest.FromJson(json);
        }
        catch (Exception ex)
        {
            var errorDto = CreateErrorDto(GeneratorErrorCodes.ParseError, GeneratorErrorMessages.ParseError, ex, bytes);
            // TODO: currently goes to dead letter; consider pulling request-id from a header
            throw new Exception(JsonSerializer.Serialize(errorDto));
        }
    }

    private static ServiceGenerateRequestDto MapRequest(Msg.GenerateRequest messagesDto, byte[] bytes)
    {
        try
        {
            return ServiceGenerateRequestMapper.ToServiceDto(messagesDto);
        }
        catch (Exception ex)
        {
            var errorDto = CreateErrorDto(GeneratorErrorCodes.MappingError, GeneratorErrorMessages.MappingError, ex, bytes);
            // TODO: currently goes to dead letter; consider pulling request-id from a header
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
                .Generate(requestDto.GenerateRequest)
                .Take(1)
                .ToArray();

            var successDto = new ServiceGenerateResponseResultSuccessDto(
                requestDto.RequestId,
                requestDto.GenerateRequest,
                generatedTimetables
            );

            var responseDto = new ServiceGenerateResponseDto(
                requestDto.RequestId,
                true,
                successDto,
                null,
                DateOnly.FromDateTime(DateTime.Now)
            );

            await PublishResponse(responseDto, replyQueue, stoppingToken);
        }
        catch (Exception ex)
        {
            var errorDto = CreateErrorDto(GeneratorErrorCodes.GenerationError, GeneratorErrorMessages.GenerationError, ex, originalBytes);

            var responseDto = new ServiceGenerateResponseDto(
                requestDto.RequestId,
                false,
                null,
                errorDto,
                DateOnly.FromDateTime(DateTime.Now)
            );

            await PublishResponse(responseDto, replyQueue, stoppingToken);
        }
    }

    private async Task PublishResponse(ServiceGenerateResponseDto responseDto, string replyQueue, CancellationToken stoppingToken)
    {
        var messagesDto = ServiceGenerateResponseMapper.ToMessagesDto(responseDto);
        var json = Msg.Serialize.ToJson(messagesDto);
        var bytes = Encoding.UTF8.GetBytes(json);
        await queuePublisher.PublishAsync(replyQueue, bytes, stoppingToken);
    }

    private static ServiceGenerateResponseResultErrorDto CreateErrorDto(
        string errorCode,
        string errorMessage,
        Exception ex,
        byte[] bytes)
    {
        return new ServiceGenerateResponseResultErrorDto(
            errorCode,
            errorMessage,
            JsonSerializer.Serialize(new { message = ex.Message, bytes })
        );
    }
}
