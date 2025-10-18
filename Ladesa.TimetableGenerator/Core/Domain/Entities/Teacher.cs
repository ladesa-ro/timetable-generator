using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Domain.Entities;

public record Teacher(string Id, IAvailabilityRule Availability);