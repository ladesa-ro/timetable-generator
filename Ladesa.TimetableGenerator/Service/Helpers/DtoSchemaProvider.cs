using NJsonSchema;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace GerarHorarioService.Helpers;

public static class DtoSchemaProvider
{
    public static async Task<string> GetJsonSchema()
    {

        var rootSchema = new JsonSchema();
        
        var DiarioSchema = JsonSchema.FromType<DiarioDto>();
        rootSchema.Definitions["Diario"] = DiarioSchema;

        var DisciplinaSchema = JsonSchema.FromType<DisciplinaDto>();
        rootSchema.Definitions["Disciplina"] = DisciplinaSchema;

        var GeradorPayloadSchema = JsonSchema.FromType<GeradorPayloadDto>();
        rootSchema.Definitions["GeradorPayload"] = GeradorPayloadSchema;

        var HorarioGeradoAulaSchema = JsonSchema.FromType<HorarioGeradoAulaDto>();
        rootSchema.Definitions["HorarioGeradoAula"] = HorarioGeradoAulaSchema;

        var HorarioGeradoSchema = JsonSchema.FromType<HorarioGeradoDto>();
        rootSchema.Definitions["HorarioGerado"] = HorarioGeradoSchema;

        var ProfessorSchema = JsonSchema.FromType<ProfessorDto>();
        rootSchema.Definitions["Professor"] = ProfessorSchema;

        var IRegraDisponibilidadeSchema = JsonSchema.FromType<IRegraDisponibilidadeDto>();
        rootSchema.Definitions["IRegraDisponibilidade"] = IRegraDisponibilidadeSchema;

        var SlotDeTempoSchema = JsonSchema.FromType<SlotDeTempoDto>();
        rootSchema.Definitions["SlotDeTempo"] = SlotDeTempoSchema;
        
        rootSchema.Type = JsonObjectType.None;
        rootSchema.Properties.Clear();

        return rootSchema.ToJson();
    }
}