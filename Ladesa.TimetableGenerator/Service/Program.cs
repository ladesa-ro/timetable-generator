using GerarHorarioService.Extensions;
using Ladesa.TimetableGenerator.Service.Features.Health.Services;
using Ladesa.TimetableGenerator.Service.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateSlimBuilder(args);

// Configuração de ambiente
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
};

// --- SWAGGER ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Timetable Generator API",
        Version = "v1",
        Description = "API para geração de horários e serviços relacionados"
    });
});


// --- DEPENDÊNCIAS ---
builder.Services.AddSingleton<RabbitMqHelpers>();
builder.Services.AddHostedService<ListenWorker>();
builder.Services.AddSingleton<IHealthService, HealthService>();

// --- CONTROLLERS ---
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Timetable Generator API v1");
    options.RoutePrefix = "api/v1/docs/swagger";
});


// --- ROTAS ---
app.MapControllers();

app.Run();