namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Constants;

public static class RabbitMqNamingConventions
{
    public const string DeadLetterExchangePrefix = "dlx.";
    public const string DeadLetterQueuePrefix = "dlq.";

    public static string GetDlxName(string queue) => $"{DeadLetterExchangePrefix}{queue}";
    public static string GetDlqName(string queue) => $"{DeadLetterQueuePrefix}{queue}";
}
