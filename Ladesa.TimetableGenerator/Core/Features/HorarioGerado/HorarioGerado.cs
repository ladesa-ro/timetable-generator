using Ladesa.TimetableGenerator.Core.Domain;
using Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

namespace Ladesa.TimetableGenerator.Core.Features.HorarioGerado;

public class HorarioGerado(
    DateOnly DataInicial,
    DateOnly DataFinal,
    
    SlotDeTempo[] HorariosDeAula,
    HorarioGeradoAula[] Aulas
);
