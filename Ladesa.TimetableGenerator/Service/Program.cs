using Ladesa.TimetableGenerator.Service.Features.Health;
using Ladesa.TimetableGenerator.Service.Features.Shared.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateSlimBuilder(args);

#region Configuração de ambiente

if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();

#endregion

#region Registro de módulos e serviços

builder.Services.AddSwaggerModule();
builder.Services.AddHealthModule();
builder.Services.AddTimetableGeneratorModule();

#endregion

#region Controllers

builder.Services.AddControllers();

#endregion

var app = builder.Build();

#region Middlewares

app.UseAppSwagger();

#endregion

#region Rotas

app.MapControllers();

#endregion

app.Run();