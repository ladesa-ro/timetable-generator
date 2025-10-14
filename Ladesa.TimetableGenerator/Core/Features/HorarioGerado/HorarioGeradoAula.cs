using Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

namespace Ladesa.TimetableGenerator.Core.Features.HorarioGerado;

public record HorarioGeradoAula(
    string TurmaId,
    string DiarioId,
    string ProfessorId,
    
    DateOnly Data,
    SlotDeTempo HorarioDeAula
);
