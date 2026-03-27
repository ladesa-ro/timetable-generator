namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Config;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration config, string key)
    {
        var value = config[key];

        if (string.IsNullOrEmpty(value))
            throw new InvalidOperationException($"Required configuration key '{key}' is missing or empty.");

        return value;
    }
}
