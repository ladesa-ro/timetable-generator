using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Logic;

public static class DisponibilidadeEvaluator
{
    public static bool VerificarDisponibilidade(
        IRegraDisponibilidade regra,
        DateOnly data,
        SlotDeTempo slot
    )
    {
        switch (regra)
        {
            case RegraDisponibilidadeAnd regraDisponibilidade:
            {
                return regraDisponibilidade.Regras.All(r => VerificarDisponibilidade(r, data, slot));
            }

            case RegraIndisponibilidadeDiaDaSemana regraDisponibilidade:
            {
                if (regraDisponibilidade.DiaDaSemana == data.DayOfWeek)
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.Slot, slot);

                return true;
            }

            case RegraIndisponibilidadeDiasDaSemana regraDisponibilidade:
            {
                return regraDisponibilidade.DiasDaSemana.Contains(data.DayOfWeek)
                       && SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.Slot, slot);
            }
            case RegraIndisponibilidadeHorario regraDisponibilidade:
            {
                return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.Slot, slot);
            }
            case RegraIndisponibilidadeDataEspecifica regraDisponibilidade:
            {
                if (data == regraDisponibilidade.Data)
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.Slot, slot);
                return true;
            }
            case RegraIndisponibilidadePeriodoDatas regraDisponibilidade:
            {
                if (data >= regraDisponibilidade.DataInicio && data <= regraDisponibilidade.DataFim)
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.Slot, slot);
                return true;
            }
            case RegraIndisponibilidadeDiaDoMes regraDisponibilidade:
            {
                if (data.Day == regraDisponibilidade.DiaDoMes)
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.Slot, slot);
                return true;
            }
            case RegraIndisponibilidadeMesesDoAno regraDisponibilidade:
            {
                if (regraDisponibilidade.Meses.Contains(data.Month))
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.Slot, slot);

                return true;
            }
            default:
            {
                return true;
            }
        }
    }
}