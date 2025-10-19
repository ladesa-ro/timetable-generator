using Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.v1.Core.Domain.Entities;

public record Teacher(string Id, IAvailabilityRule AvailabilityRule);