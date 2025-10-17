using Ladesa.TimetableGenerator.Service.Features.Health;
using Ladesa.TimetableGenerator.Service.Features.Shared.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;

namespace Ladesa.TimetableGenerator.Service.Features;

public static class FeaturesModule
{
    public static IServiceCollection AddFeaturesModule(this IServiceCollection services)
    {
        services.AddSwaggerModule();
        services.AddHealthModule();
        services.AddTimetableGeneratorModule();
        
        return services;
    }

    public static WebApplication UseAppFeatures(this WebApplication app)
    {
        app.UseAppSwagger();
        app.UseAppHealth();

        return app;
    }
}