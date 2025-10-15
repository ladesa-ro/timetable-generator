using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class GeradorPayloadMapper
{
    public static GeradorPayload ToDomain(GeradorPayloadDto dto)
    {
        return new GeradorPayload(
            RequestId: dto.RequestId,
            DataInicial: DateOnly.FromDateTime(dto.DataInicial.Date),
            DataFinal: DateOnly.FromDateTime(dto.DataFinal.Date),
            Turmas: dto.Turmas.Select(TurmaMapper.ToDomain).ToArray(),
            Professores: dto.Professores.Select(ProfessorMapper.ToDomain).ToArray(),
            Diarios: dto.Diarios.Select(DiarioMapper.ToDomain).ToArray(),
            HorariosDeAula: dto.HorariosDeAula.Select(SlotDeTempoMapper.ToDomain).ToArray()
        );
    }

    public static GeradorPayloadDto ToDto(GeradorPayload domain)
    {
        return new GeradorPayloadDto()
        {
            RequestId = domain.RequestId,
            DataInicial = domain.DataInicial.ToDateTime(TimeOnly.MinValue),
            DataFinal = domain.DataFinal.ToDateTime(TimeOnly.MinValue),
            Turmas = domain.Turmas.Select(TurmaMapper.ToDto).ToArray(),
            Professores = domain.Professores.Select(ProfessorMapper.ToDto).ToArray(),
            Diarios = domain.Diarios.Select(DiarioMapper.ToDto).ToArray(),
            HorariosDeAula = domain.HorariosDeAula.Select(SlotDeTempoMapper.ToDto).ToArray()
        };
    }
}