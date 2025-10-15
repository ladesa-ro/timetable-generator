using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class RegraDisponibilidadeMapper
{
    public static IRegraDisponibilidadeDto ToDto(IRegraDisponibilidade domain)
        => domain switch
        {
            RegraDisponibilidadeAnd r => new RegraDisponibilidadeAndDto(r.Regras.Select(ToDto).ToArray()),
            RegraIndisponibilidadeDiaDaSemana r => new RegraIndisponibilidadeDiaDaSemanaDto(r.DiaDaSemana, SlotDeTempoMapper.ToDto(r.Slot)),
            RegraIndisponibilidadeDiasDaSemana r => new RegraIndisponibilidadeDiasDaSemanaDto(r.DiasDaSemana, SlotDeTempoMapper.ToDto(r.Slot)),
            RegraIndisponibilidadeHorario r => new RegraIndisponibilidadeHorarioDto(SlotDeTempoMapper.ToDto(r.Slot)),
            RegraIndisponibilidadeDataEspecifica r => new RegraIndisponibilidadeDataEspecificaDto(r.Data, SlotDeTempoMapper.ToDto(r.Slot)),
            RegraIndisponibilidadePeriodoDatas r => new RegraIndisponibilidadePeriodoDatasDto(r.DataInicio, r.DataFim, SlotDeTempoMapper.ToDto(r.Slot)),
            RegraIndisponibilidadeDiaDoMes r => new RegraIndisponibilidadeDiaDoMesDto(r.DiaDoMes, SlotDeTempoMapper.ToDto(r.Slot)),
            RegraIndisponibilidadeMesesDoAno r => new RegraIndisponibilidadeMesesDoAnoDto(r.Meses, SlotDeTempoMapper.ToDto(r.Slot)),
            _ => throw new ArgumentOutOfRangeException(nameof(domain))
        };

    public static IRegraDisponibilidade ToDomain(IRegraDisponibilidadeDto dto)
        => dto switch
        {
            RegraDisponibilidadeAndDto r => new RegraDisponibilidadeAnd(r.Regras.Select(ToDomain).ToArray()),
            RegraIndisponibilidadeDiaDaSemanaDto r => new RegraIndisponibilidadeDiaDaSemana(r.DiaDaSemana, SlotDeTempoMapper.ToDomain(r.Slot)),
            RegraIndisponibilidadeDiasDaSemanaDto r => new RegraIndisponibilidadeDiasDaSemana(r.DiasDaSemana, SlotDeTempoMapper.ToDomain(r.Slot)),
            RegraIndisponibilidadeHorarioDto r => new RegraIndisponibilidadeHorario(SlotDeTempoMapper.ToDomain(r.Slot)),
            RegraIndisponibilidadeDataEspecificaDto r => new RegraIndisponibilidadeDataEspecifica(r.Data, SlotDeTempoMapper.ToDomain(r.Slot)),
            RegraIndisponibilidadePeriodoDatasDto r => new RegraIndisponibilidadePeriodoDatas(r.DataInicio, r.DataFim, SlotDeTempoMapper.ToDomain(r.Slot)),
            RegraIndisponibilidadeDiaDoMesDto r => new RegraIndisponibilidadeDiaDoMes(r.DiaDoMes, SlotDeTempoMapper.ToDomain(r.Slot)),
            RegraIndisponibilidadeMesesDoAnoDto r => new RegraIndisponibilidadeMesesDoAno(r.Meses, SlotDeTempoMapper.ToDomain(r.Slot)),
            _ => throw new ArgumentOutOfRangeException(nameof(dto))
        };
}
