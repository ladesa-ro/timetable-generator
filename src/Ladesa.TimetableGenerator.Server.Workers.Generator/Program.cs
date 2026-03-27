using Ladesa.TimetableGenerator.Server.Workers.Generator;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.ConfigureServices(context.Configuration);
});

var host = builder.Build();

host.Run();
