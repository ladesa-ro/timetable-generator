using Ladesa.TimetableGenerator.Server.Api;

var builder = WebApplication.CreateSlimBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.Configure();

app.Run();
