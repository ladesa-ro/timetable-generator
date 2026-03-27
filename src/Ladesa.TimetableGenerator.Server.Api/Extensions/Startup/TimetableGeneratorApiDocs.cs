using Microsoft.OpenApi.Models;

namespace Ladesa.TimetableGenerator.Server.Api.Extensions.Startup;

public static class TimetableGeneratorApiDocsExtensions
{
    public static IServiceCollection AddTimetableGeneratorApiDocs(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Timetable Generator API",
                Description = "API para geracao de horarios e servicos relacionados"
            });
        });

        return services;
    }

    public static WebApplication UseSwaggerDocs(this WebApplication app)
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
