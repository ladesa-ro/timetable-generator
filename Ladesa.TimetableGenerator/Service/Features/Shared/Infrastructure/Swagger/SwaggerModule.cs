using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Ladesa.TimetableGenerator.Service.Features.Shared.Infrastructure.Swagger;

public static class SwaggerModule
{
    public static IServiceCollection AddSwaggerModule(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Timetable Generator API",
                Version = "v1",
                Description = "API para geração de horários e serviços relacionados"
            });
        });

        return services;
    }

    public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Timetable Generator API v1");
            options.RoutePrefix = "api/v1/docs/swagger";
        });


        return app;
    }
}