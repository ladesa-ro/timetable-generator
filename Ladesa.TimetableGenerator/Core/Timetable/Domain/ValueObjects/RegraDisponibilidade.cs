namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

public enum TipoRegraDisponibilidade
{
    RegraDisponibilidadeAnd,
    RegraIndisponibilidadeDiaDaSemana,
    RegraIndisponibilidadeDiasDaSemana,
    RegraIndisponibilidadeHorario,
    RegraIndisponibilidadeDataEspecifica,
    RegraIndisponibilidadePeriodoDatas,
    RegraIndisponibilidadeDiaDoMes,
    RegraIndisponibilidadeMesesDoAno,
}

public interface IRegraDisponibilidade { }

public record RegraDisponibilidadeAnd(IRegraDisponibilidade[] Regras) : IRegraDisponibilidade;

public record RegraIndisponibilidadeDiaDaSemana(DayOfWeek DiaDaSemana, SlotDeTempo Slot)
    : IRegraDisponibilidade;

public record RegraIndisponibilidadeDiasDaSemana(DayOfWeek[] DiasDaSemana, SlotDeTempo Slot)
    : IRegraDisponibilidade;

public record RegraIndisponibilidadeHorario(SlotDeTempo Slot) : IRegraDisponibilidade;

public record RegraIndisponibilidadeDataEspecifica(DateOnly Data, SlotDeTempo Slot)
    : IRegraDisponibilidade;

public record RegraIndisponibilidadePeriodoDatas(
    DateOnly DataInicio,
    DateOnly DataFim,
    SlotDeTempo Slot
) : IRegraDisponibilidade;

public record RegraIndisponibilidadeDiaDoMes(int DiaDoMes, SlotDeTempo Slot)
    : IRegraDisponibilidade;

public record RegraIndisponibilidadeMesesDoAno(int[] Meses, SlotDeTempo Slot)
    : IRegraDisponibilidade;
