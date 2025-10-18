using Ladesa.TimetableGenerator.v1.Service.Features.Health;
using Ladesa.TimetableGenerator.v1.Service.Features.Shared.Infrastructure.RabbitMq;
using Ladesa.TimetableGenerator.v1.Service.Features.Shared.Infrastructure.Swagger;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator;
using Microsoft.AspNetCore.Builder;

namespace Ladesa.TimetableGenerator.v1.Service.Features;

public static class FeaturesModule
{
    public static IServiceCollection AddModuleFeatures(this IServiceCollection services)
    {
        services.AddModuleInfrastructureRabbitMq();

        services.AddSwaggerModule();
        services.AddHealthModule();
        services.AddModuleTimetableGenerator();

        return services;
    }

    public static WebApplication UseAppFeatures(this WebApplication app)
    {
        app.UseAppSwagger();
        app.UseAppHealth();

        return app;
    }
}