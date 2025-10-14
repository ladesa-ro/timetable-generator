using Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

namespace Ladesa.TimetableGenerator.Core;

public class HelperRegraDisponibilidade
{
    /// <summary>
    ///     UTILITÁRIO: Verifica que um (diaSemanaIso, intervalo)
    ///     pode ocorrer num conjunto de disponibilidades.
    /// </summary>
    public static bool Execute(
        IRegraDisponibilidade regraDisponibilidade,
        DateOnly data,
        SlotDeTempo slotDeTempo
    )
    {
        return regraDisponibilidade.VerificarDisponibilidade(data, slotDeTempo);
    }
}