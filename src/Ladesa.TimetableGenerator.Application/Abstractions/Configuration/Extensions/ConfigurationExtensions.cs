using Microsoft.Extensions.Configuration;

namespace Ladesa.TimetableGenerator.Application.Abstractions.Configuration.Extensions;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration config, string key)
    {
        var value = config[key];

        return string.IsNullOrEmpty(value) ? throw new InvalidOperationException($"Required configuration key '{key}' is missing or empty.") : value;
    }
}
