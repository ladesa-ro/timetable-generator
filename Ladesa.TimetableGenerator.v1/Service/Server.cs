using Ladesa.TimetableGenerator.v1.Service.Features.Generator;
using Ladesa.TimetableGenerator.v1.Service.Features.Generator.Config;
using Ladesa.TimetableGenerator.v1.Service.Features.Health;
using Ladesa.TimetableGenerator.v1.Service.Features.Health.Ports;
using Ladesa.TimetableGenerator.v1.Service.Shared.Application.Ports;
using Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Config;
using Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Connection;
using Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.OpenApi.Models;

namespace Ladesa.TimetableGenerator.v1.Service;

public class Server
{
    public static void Start(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);

        if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();

        builder.Services.Configure<RouteOptions>(options =>
        {
            options.SetParameterPolicy<RegexInlineRouteConstraint>("regex");
        });


        AddModuleFeatures(builder.Services);

        var app = builder.Build();
        UseAppFeatures(app);

        app.Run();
    }
    
    public static void AddModuleFeatures(IServiceCollection services)
    {
        // RABBIT MQ
        services.AddSingleton<IRabbitMqConfigProvider, RabbitMqConfigEnvironmentImpl>();
        services.AddSingleton<RabbitMqConnectionFactoryImpl>();

        services.AddSingleton<RabbitMqPersistentConnectionImpl>();

        services.AddSingleton<IQueueListener, RabbitMqQueueListenerImpl>();
        services.AddSingleton<IQueuePublisher, RabbitMqQueuePublisherImpl>();
        services.AddSingleton<IDeadLetterHandler, RabbitMqDeadLetterHandlerImpl>();
        // END RABBIT MQ

        // SWAGGER
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Timetable Generator API",
                Description = "API para geração de horários e serviços relacionados"
            });
        });
        // END SWAGGER
    
        // HEALTH
        services.AddScoped<IHealthService, HealthService>();
        // END HEALTH
    
        // GENERATOR
        services.AddSingleton<IGeneratorListenWorkerConfig, GeneratorListerWorkerConfigEnvironmentImpl>();
        services.AddHostedService<GeneratorListenWorker>();
        // END GENERATOR
    }
    
    public static void UseAppFeatures(WebApplication app)
    {
        // SWAGGER 
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Timetable Generator API v1");
            options.RoutePrefix = "api/v1/docs/swagger";
        });
        // END SWAGGER
    
        // HEALTH
        app.MapHealthEndpoints();
        // END HEALTH
    }
}