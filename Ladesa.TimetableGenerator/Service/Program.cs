using System.Text.Json;
using GerarHorarioService.Extensions;
using GerarHorarioService.Helpers;
using Ladesa.TimetableGenerator.Service.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateSlimBuilder(args);

if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddSingleton<RabbitMqHelpers>();
builder.Services.AddHostedService<ListenWorker>();

var app = builder.Build();

app.MapGet(
    "/health",
    () =>
    {
        var status = new
        {
            status = "ok",
            service = "timetable-generator",
            timestamp = DateTimeOffset.UtcNow
        };

        return Results.Ok(status);
    }
);

app.MapGet(
    "/schemas",
    async () =>
    {
        var schema = await DtoSchemaProvider.GetJsonSchema();
        return Results.Json(JsonSerializer.Deserialize<object>(schema));
    }
);

app.Run();