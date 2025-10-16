using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record Turma(string Id, IRegraDisponibilidade RegraDisponibilidade);
