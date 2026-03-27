using Ladesa.TimetableGenerator.Server.Api.Endpoints;
using Ladesa.TimetableGenerator.Server.Api.Extensions.Startup;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Ladesa.TimetableGenerator.Server.Api;

public static class Server
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.SetParameterPolicy<RegexInlineRouteConstraint>("regex");
        });

        services
            .AddTimetableGeneratorApplication()
            .AddTimetableGeneratorApiDocs();

        return services;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.UseSwaggerDocs();
        app.MapHealthEndpoints();

        return app;
    }
}
