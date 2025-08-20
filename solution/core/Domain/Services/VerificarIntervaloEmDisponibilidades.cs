using Ladesa.TimetableGenerator.Core.Domain;

namespace Ladesa.TimetableGenerator.Core;

public partial class VerificarIntervaloEmDisponibilidades
{
    ///<summary>
    /// UTILITÁRIO: Verifica que um (diaSemanaIso, intervalo)
    /// pode ocorrer num conjunto de disponibilidades.
    ///</summary>
    public static bool Execute(
        IRegraDisponibilidade regraDisponibilidade,
        DateOnly data,
        IntervaloDeTempo intervaloDeTempo
    )
    {
        return regraDisponibilidade.VerificarDisponibilidade(data, intervaloDeTempo);
    }
}