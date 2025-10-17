using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record Group(string Id, AvailabilityRule AvailabilityRule);