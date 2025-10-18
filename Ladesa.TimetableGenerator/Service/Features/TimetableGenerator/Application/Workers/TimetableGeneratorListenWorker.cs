using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;
using Ladesa.TimetableGenerator.Features.Gerador;
using Ladesa.TimetableGenerator.Service.Features.Shared.Application.Ports;
using Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.Messages;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Workers;

public class TimetableGeneratorListenWorker(IQueueListener queueListener, IQueuePublisher queuePublisher) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await queueListener.SubscribeAsync(
            "gerar_horario",
            async bytes =>
            {
                try
                {

                    var dto = GeneratorPayloadDto.Parser.ParseFrom(bytes);
                    
                    var generatorPayload = GeneratorPayloadMapper.ToDomain(dto);

                    var horariosIterable = Gerador.GerarHorario(generatorPayload);
                    var horarios = horariosIterable.Take(1).ToArray();

                    var response = new GeneratorResponse(
                        Success: true,
                        Message: "Successfully generated timetable",
                        GeneratedTimetables: horarios,
                        Date: DateOnly.FromDateTime(DateTime.UtcNow)
                    );

                    var responseDto = GeneratorResponseMapper.ToDto(response);

                    byte[] responseDtoBytes;
                    using (var ms = new MemoryStream())
                    {
                        var cos = new Google.Protobuf.CodedOutputStream(ms, leaveOpen: true);
                        responseDto.WriteTo(cos);
                        cos.Flush();
                        responseDtoBytes = ms.ToArray();
                    }

                    await queuePublisher.PublishAsync("horario_gerado", responseDtoBytes, cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    // Cria mensagem de erro para enviar de volta ou logar
                    var errorResponse = new GeneratorResponse(
                        Success: false,
                        Message: $"Erro ao processar a mensagem: {ex.Message}",
                        GeneratedTimetables: [],
                        Date: DateOnly.FromDateTime(DateTime.UtcNow)
                    );

                    var errorDto = GeneratorResponseMapper.ToDto(errorResponse);

                    byte[] errorBytes;
                    using (var ms = new MemoryStream())
                    {
                        var cos = new Google.Protobuf.CodedOutputStream(ms, leaveOpen: true);
                        errorDto.WriteTo(cos);
                        cos.Flush();
                        errorBytes = ms.ToArray();
                    }

                    // Publica na fila de erro
                    await queuePublisher.PublishAsync("horario_erro", errorBytes, cancellationToken: stoppingToken);
                }
            },
            stoppingToken
        );
    }
}
