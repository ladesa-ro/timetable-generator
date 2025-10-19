using System.Text.Json;
using Google.Protobuf;
using Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Core;
using Ladesa.TimetableGenerator.v1.Service.Features.Shared.Application.Ports;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.DTOs;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Protobuf.Mappers;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.Workers;

public class TimetableGeneratorListenWorker(IQueueListener queueListener, IQueuePublisher queuePublisher)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await queueListener.SubscribeAsync(
            "gerar_horario",
            async bytes =>
            {
                Protobuf.ServiceGenerateRequestDto serviceGenerateRequestDtoProtobuf;

                try
                {
                    serviceGenerateRequestDtoProtobuf = Protobuf.ServiceGenerateRequestDto.Parser.ParseFrom(bytes);
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
                        ServiceGenerateRequestMapper.ToServiceTimetableGeneratorDto(serviceGenerateRequestDtoProtobuf);
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
                        Generator.GenerateTimetables(serviceGenerateRequestDto.GenerateRequest);

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

                    await queuePublisher.PublishAsync("horario_gerado", responseDtoProtobufBytes, stoppingToken);
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

                    await queuePublisher.PublishAsync("horario_gerado", responseDtoProtobufBytes, stoppingToken);
                }
            },
            stoppingToken
        );
    }
}