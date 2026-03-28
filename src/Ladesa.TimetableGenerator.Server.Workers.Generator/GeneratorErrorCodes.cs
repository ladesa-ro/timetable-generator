namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

/// <summary>
/// Error codes used in the Generator handler for categorizing failures.
/// </summary>
public static class GeneratorErrorCodes
{
    /// <summary>
    /// Error parsing the JSON timetableCommand payload.
    /// </summary>
    public const string ParseError = "GEN-0001-PARSE";

    /// <summary>
    /// Error mapping the timetableCommand to internal DTO.
    /// </summary>
    public const string MappingError = "GEN-0002-MAP";

    /// <summary>
    /// Error during timetable generation.
    /// </summary>
    public const string GenerationError = "GEN-0003-GEN";
}

/// <summary>
/// Error messages used in the Generator handler (Portuguese).
/// </summary>
public static class GeneratorErrorMessages
{
    /// <summary>
    /// Message for parse errors.
    /// </summary>
    public const string ParseError = "Erro ao tentar parsear o timetableCommand";

    /// <summary>
    /// Message for mapping errors.
    /// </summary>
    public const string MappingError = "Erro ao tentar converter timetableCommand para dto";

    /// <summary>
    /// Message for generation errors.
    /// </summary>
    public const string GenerationError = "Erro ao gerar horario";
}
