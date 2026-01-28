namespace ClassInsight.Domain.Entities;

public class Insight
{
    public Guid Id { get; private set; }
    public string AlunoIdHash { get; private set; } = null!;
    public string TextoRelato { get; private set; } = null!;
    public string Disciplina { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }

    // Construtor necessário para criar o objeto
    public Insight(string alunoIdHash, string textoRelato, string disciplina)
    {
        Id = Guid.NewGuid();
        AlunoIdHash = alunoIdHash;
        TextoRelato = textoRelato;
        Disciplina = disciplina;
        DataCriacao = DateTime.UtcNow;
    }

    // Construtor vazio para o Entity Framework (importante para o futuro)
    protected Insight() { }
}