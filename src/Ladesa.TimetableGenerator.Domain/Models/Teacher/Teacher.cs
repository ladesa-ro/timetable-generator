using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Abstractions.Entities.Interfaces;

namespace Ladesa.TimetableGenerator.Domain.Models.Teacher;

public record Teacher(string Id, Availability.Availability Availability) : IHasId;