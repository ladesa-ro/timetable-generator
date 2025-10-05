using GerarHorarioService.Extensions;
using GerarHorarioService.Workers;

var builder = Host.CreateApplicationBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddSingleton<RabbitMqHelpers>();

builder.Services.AddHostedService<ListenWorker>();

var host = builder.Build();
host.Run();
