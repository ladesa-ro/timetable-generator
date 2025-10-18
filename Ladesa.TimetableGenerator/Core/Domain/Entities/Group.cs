using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Domain.Entities;

public record Group(string Id, IAvailabilityRule AvailabilityRule);