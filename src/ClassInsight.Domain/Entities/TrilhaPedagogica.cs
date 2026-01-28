using ClassInsight.Domain.Enums;
using ClassInsight.Domain.ValueObjects;

namespace ClassInsight.Domain.Entities;

public class TrilhaPedagogica
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string AlunoIdHash { get; private set; } = null!;
    public string TopicoFoco { get; private set; } = null!;
    public List<PassoTrilha> Passos { get; private set; } = new();
    public DateTime DataGeracao { get; private set; } = DateTime.UtcNow;

    private TrilhaPedagogica() { }

    public TrilhaPedagogica(string alunoIdHash, string topico)
    {
        AlunoIdHash = alunoIdHash;
        TopicoFoco = topico;
    }

    public void AdicionarPassos(List<PassoTrilha> passos)
    {
        if (passos == null || !passos.Any())
            throw new ArgumentException("A trilha precisa ter passos.");
            
        Passos.AddRange(passos);
    }
}