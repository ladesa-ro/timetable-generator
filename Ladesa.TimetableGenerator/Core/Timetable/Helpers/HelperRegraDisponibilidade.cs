using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;
using Ladesa.TimetableGenerator.Core.Timetable.Logic;

namespace Ladesa.TimetableGenerator.Core;

public class HelperRegraDisponibilidade
{
    /// <summary>
    ///     UTILITÁRIO: Verifica que um (diaSemanaIso, intervalo)
    ///     pode ocorrer num conjunto de disponibilidades.
    /// </summary>
    public static bool Execute(
        RegraDisponibilidade regraDisponibilidade,
        DateOnly data,
        SlotDeTempo slotDeTempo
    )
    {
        return DisponibilidadeEvaluator.VerificarDisponibilidade(regraDisponibilidade, data, slotDeTempo);
    }
}