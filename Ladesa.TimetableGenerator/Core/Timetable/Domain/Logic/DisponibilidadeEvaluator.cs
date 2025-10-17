using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Logic;

public static class DisponibilidadeEvaluator
{
    public static bool VerificarDisponibilidade(
        AvailabilityRule regra,
        DateOnly data,
        TimeSlot timeSlot
    )
    {
        switch (regra)
        {
            case AvailabilityRuleCompound regraDisponibilidade:
            {
                return regraDisponibilidade.Rules.All(r =>
                    VerificarDisponibilidade(r, data, timeSlot)
                );
            }

            case AvailabilityRuleUnavailableWeekDay regraDisponibilidade:
            {
                if (regraDisponibilidade.WeekDay == data.DayOfWeek)
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.TimeSlot, timeSlot);

                return true;
            }

            case AvailabilityRuleUnavailableWeekDays regraDisponibilidade:
            {
                return regraDisponibilidade.WeekDays.Contains(data.DayOfWeek)
                       && SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.TimeSlot, timeSlot);
            }
            case AvailabilityRuleUnavailableTimeSlot regraDisponibilidade:
            {
                return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.TimeSlot, timeSlot);
            }
            case AvailabilityRuleUnavailableSpecificDate regraDisponibilidade:
            {
                if (data == regraDisponibilidade.Date)
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.TimeSlot, timeSlot);
                return true;
            }
            case AvailabilityRuleUnavailableDateRange regraDisponibilidade:
            {
                if (data >= regraDisponibilidade.Start && data <= regraDisponibilidade.End)
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.TimeSlot, timeSlot);
                return true;
            }
            case AvailabilityRuleUnavailableMonthDay regraDisponibilidade:
            {
                if (data.Day == regraDisponibilidade.MonthDay)
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.TimeSlot, timeSlot);
                return true;
            }
            case AvailabilityRuleUnavailableYearMonths regraDisponibilidade:
            {
                if (regraDisponibilidade.Months.Contains(data.Month))
                    return SlotDeTempoEvaluator.VerificarIntervalo(regraDisponibilidade.TimeSlot, timeSlot);

                return true;
            }
            default:
            {
                return true;
            }
        }
    }
}