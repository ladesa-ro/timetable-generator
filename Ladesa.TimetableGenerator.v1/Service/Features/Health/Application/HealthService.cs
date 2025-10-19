using Ladesa.TimetableGenerator.v1.Core.Application.DTOs;
using Ladesa.TimetableGenerator.v1.Core.Domain.Entities;
using Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.DTOs;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Protobuf.Mappers;
using Google.Protobuf;

namespace Ladesa.TimetableGenerator.v1.Service.Features.Health.Application;

public class HealthService : IHealthService
{
    public object GetStatus()
    {
        return new
        {
            status = "up",
            service = "timetable-generator",
            timestamp = DateTimeOffset.UtcNow
        };
    }
}