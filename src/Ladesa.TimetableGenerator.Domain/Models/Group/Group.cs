using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Abstractions.Entities.Interfaces;

namespace Ladesa.TimetableGenerator.Domain.Models.Group;

public record Group(string Id, Availability.Availability Availability) : IHasId;