using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;
using Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.Entities;
using Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.ValueObjects;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.Messages;

public static class GeneratorResponseMapper
{
    public static GeneratorResponse ToDomain(GeneratorResponseDto dto)
    {
        return new GeneratorResponse(
            dto.Success,
            dto.Message,
            dto.GeneratedTimetables.Select(GeneratedTimetableMapper.ToDomain).ToArray(),
            DateOnly.Parse(dto.Date)
        );
    }

    public static GeneratorResponseDto ToDto(GeneratorResponse domain)
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