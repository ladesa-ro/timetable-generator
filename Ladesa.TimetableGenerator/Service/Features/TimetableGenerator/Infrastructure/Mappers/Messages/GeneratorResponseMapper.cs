using Ladesa.TimetableGenerator.Core.Application.DTOs;
using Ladesa.TimetableGenerator.Service.Features.TimetableGenerator.Infrastructure.Mappers.Entities;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Features.TimetableGenerator.Infrastructure.Mappers.Messages;

public static class GeneratorResponseMapper
{
    public static GenerateResponse ToDomain(GeneratorResponseDto dto)
    {
        return new GenerateResponse(
            dto.Success,
            dto.Message,
            dto.GeneratedTimetables.Select(GeneratedTimetableMapper.ToDomain).ToArray(),
            DateOnly.Parse(dto.Date)
        );
    }

    public static GeneratorResponseDto ToDto(GenerateResponse domain)
    {
        var dto = new GeneratorResponseDto
        {
            Success = domain.Success,
            Message = domain.Message,
            Date = domain.Date.ToString()
        };

        dto.GeneratedTimetables.AddRange(domain.GeneratedTimetables.Select(GeneratedTimetableMapper.ToDto).ToArray());

        return dto;
    }
}