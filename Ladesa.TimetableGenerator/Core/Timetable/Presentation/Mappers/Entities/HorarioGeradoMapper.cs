using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class HorarioGeradoMapper
{
    public static HorarioGerado ToDomain(HorarioGeradoDto dto)
    {
        return new HorarioGerado(
            RequestId: dto.RequestId,
            DataInicial: DateOnly.FromDateTime(dto.DataInicial.Date),
            DataFinal: DateOnly.FromDateTime(dto.DataFinal.Date),
            HorariosDeAula: dto.HorariosDeAula.Select(SlotDeTempoMapper.ToDomain).ToArray(),
            Aulas: dto.Aulas.Select(HorarioGeradoAulaMapper.ToDomain).ToArray(),
            Score: dto.Score
        );
    }

    public static HorarioGeradoDto ToDto(HorarioGerado domain)
    {
        return new HorarioGeradoDto()
        {
            RequestId = domain.RequestId,
            DataInicial = domain.DataInicial.ToDateTime(TimeOnly.MinValue),
            DataFinal = domain.DataFinal.ToDateTime(TimeOnly.MinValue),
            HorariosDeAula = domain.HorariosDeAula.Select(SlotDeTempoMapper.ToDto).ToArray(),
            Aulas = domain.Aulas.Select(HorarioGeradoAulaMapper.ToDto).ToArray(),
            Score = domain.Score,
        };
    }
}
