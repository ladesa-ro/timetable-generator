using Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

namespace Ladesa.TimetableGenerator.Core.Features.Payload;

public interface IGeradorPayload
{
    public DateOnly DataInicial { get; }
    public DateOnly DataFinal { get; }
    
    public Turma[] Turmas { get;  }

    public Professor[] Professores { get;  }
    
    public Diario[] Diarios { get;  }
    
    public SlotDeTempo[] HorariosDeAula { get; }
}
