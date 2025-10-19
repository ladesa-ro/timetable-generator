using Ladesa.TimetableGenerator.v1.Service.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;

var builder = WebApplication.CreateSlimBuilder(args);

#region Configuração de ambiente

if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();

builder.Services.Configure<RouteOptions>(options =>
{
    options.SetParameterPolicy<RegexInlineRouteConstraint>("regex");
});

#endregion

builder.Services.AddModuleFeatures();

var app = builder.Build();
app.UseAppFeatures();

app.Run();