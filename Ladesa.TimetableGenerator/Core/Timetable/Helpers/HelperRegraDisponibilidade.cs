using Ladesa.TimetableGenerator.Core.Timetable.Domain.Logic;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core;

public class HelperRegraDisponibilidade
{
    /// <summary>
    ///     UTILITÁRIO: Verifica que um (diaSemanaIso, intervalo)
    ///     pode ocorrer num conjunto de disponibilidades.
    /// </summary>
    public static bool Execute(
        RegraDisponibilidadeAnd regraDisponibilidadeAndAnd,
        DateOnly data,
        SlotDeTempo slotDeTempo
    )
    {
        return DisponibilidadeEvaluator.VerificarDisponibilidade(
            regraDisponibilidadeAndAnd,
            data,
            slotDeTempo
        );
    }
}
