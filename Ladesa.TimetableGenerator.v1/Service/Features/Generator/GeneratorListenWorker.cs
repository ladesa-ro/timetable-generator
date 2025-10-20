using System.Text.Json;
using Google.Protobuf;
using Ladesa.TimetableGenerator.v1.Service.Features.Generator.Config;
using Ladesa.TimetableGenerator.v1.Service.Features.Generator.DTOs;
using Ladesa.TimetableGenerator.v1.Service.Shared.Application.Ports;
using Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

namespace Ladesa.TimetableGenerator.v1.Service.Features.Generator;

public class GeneratorListenWorker(IGeneratorListenWorkerConfig generatorListenWorkerConfig, IQueueListener queueListener, IQueuePublisher queuePublisher)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = generatorListenWorkerConfig.GetConfig();
        await queueListener.SubscribeAsync(
            config.QueueListen,
            async bytes =>
            {
                Protobuf.ServiceGenerateRequest serviceGenerateRequestDtoProtobuf;

                try
                {
                    serviceGenerateRequestDtoProtobuf = Protobuf.ServiceGenerateRequest.Parser.ParseFrom(bytes);
                }
                catch (Exception ex)
                {
                    var errorDto = new ServiceGenerateResponseResultErrorDto(
                        "0001-parse-err",
                        "Erro ao tentar parsear o request",
                        JsonSerializer.Serialize(new { message = ex.Message, bytes = bytes })
                    );

                    // TODO: atualmente vai para a dead letter, porem pensar em puxar o request-id de algum header
                    throw new Exception(JsonSerializer.Serialize(errorDto));
                }

                ServiceGenerateRequestDto serviceGenerateRequestDto;

                try
                {
                    serviceGenerateRequestDto =
                        ServiceGenerateRequestMapper.ToServiceDto(serviceGenerateRequestDtoProtobuf);
                }
                catch (Exception ex)
                {
                    var errorDto = new ServiceGenerateResponseResultErrorDto(
                        "0002-map-err",
                        "Erro ao tentar converter request para dto",
                        JsonSerializer.Serialize(new { message = ex.Message, bytes = bytes })
                    );

                    // TODO: atualmente vai para a dead letter, porem pensar em puxar o request-id de algum header
                    throw new Exception(JsonSerializer.Serialize(errorDto));
                }

                try
                {
                    var generatedTimetablesIterable =
                        Core.Generator.Generator.GenerateTimetables(serviceGenerateRequestDto.GenerateRequest);

                    var generatedTimetables = generatedTimetablesIterable.Take(1).ToArray();

                    var successDto = new ServiceGenerateResponseResultSuccessDto(
                        serviceGenerateRequestDto.RequestId,
                        serviceGenerateRequestDto.GenerateRequest,
                        generatedTimetables
                    );

                    var responseDto = new ServiceGenerateResponseDto(
                        serviceGenerateRequestDto.RequestId,
                        true,
                        successDto,
                        null,
                        DateOnly.FromDateTime(DateTime.Now)
                    );

                    var responseDtoProtobuf = ServiceGenerateResponseMapper.ToProtobufDto(responseDto);

                    var responseDtoProtobufBytes = responseDtoProtobuf.ToByteArray();

                    await queuePublisher.PublishAsync(config.QueueReply, responseDtoProtobufBytes, stoppingToken);
                }
                catch (Exception ex)
                {
                    var errorDto = new ServiceGenerateResponseResultErrorDto(
                        "0003-gen-err",
                        "Erro ao gerar horário",
                        JsonSerializer.Serialize(new { message = ex.Message, bytes = bytes })
                    );

                    var responseDto = new ServiceGenerateResponseDto(
                        serviceGenerateRequestDto.RequestId,
                        false,
                        null,
                        errorDto,
                        DateOnly.FromDateTime(DateTime.Now)
                    );

                    var responseDtoProtobuf = ServiceGenerateResponseMapper.ToProtobufDto(responseDto);

                    var responseDtoProtobufBytes = responseDtoProtobuf.ToByteArray();

                    await queuePublisher.PublishAsync(config.QueueReply, responseDtoProtobufBytes, stoppingToken);
                }
            },
            stoppingToken
        );
    }
}