using Ladesa.TimetableGenerator.v1.Service.Generator;
using Ladesa.TimetableGenerator.v1.Service.Health;
using Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq;
using Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;

namespace Ladesa.TimetableGenerator.v1.Service;

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