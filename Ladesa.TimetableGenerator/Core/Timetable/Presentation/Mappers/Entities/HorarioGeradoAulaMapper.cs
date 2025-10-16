using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class HorarioGeradoAulaMapper
{
    public static HorarioGeradoAula ToDomain(HorarioGeradoAulaDto dto)
    {
        return new HorarioGeradoAula(
            TurmaId: dto.TurmaId,
            DiarioId: dto.DiarioId,
            ProfessorId: dto.ProfessorId,
            Data: DateOnly.FromDateTime(dto.Data.Date),
            HorarioDeAula: SlotDeTempoMapper.ToDomain(dto.HorarioDeAula)
        );
    }

    public static HorarioGeradoAulaDto ToDto(HorarioGeradoAula domain)
    {
        return new HorarioGeradoAulaDto()
        {
            TurmaId = domain.TurmaId,
            DiarioId = domain.DiarioId,
            ProfessorId = domain.ProfessorId,
            Data = domain.Data.ToDateTime(TimeOnly.MinValue),
            HorarioDeAula = SlotDeTempoMapper.ToDto(domain.HorarioDeAula),
        };
    }
}
