namespace Sisgea.GerarHorario.Core.Dtos.Entidades;

public record Professor
{
    public string Id { get; init; }
    public string? Nome { get; init; }
    public int DiaPRD {get; set;}
    public Intervalo IntervaloEscolhido {get; set;}
    public int DiaAulaEscolhido {get; set;}
    public DisponibilidadeDia[] Disponibilidades { get; init; }

    public Professor(string id, string nome, DisponibilidadeDia[] disponibilidades)
    {
        Id = id;
        Nome = nome;
        Disponibilidades = disponibilidades;
    }
}
