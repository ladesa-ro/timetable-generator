using Ladesa.TimetableGenerator.Service.Features.Health.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ladesa.TimetableGenerator.Service.Features.Health.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController(IHealthService healthService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var status = healthService.GetStatus();
        return Ok(status);
    }
}
