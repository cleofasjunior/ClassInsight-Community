using ClassInsight.Domain.ValueObjects;
using ClassInsight.Domain.Interfaces;

namespace ClassInsight.Domain.Entities;

public class RegistroAprendizagem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string AlunoIdHash { get; private set; } = null!;// Anonimizado
    public string TextoOriginal { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    
    // Resultados da IA (Começam nulos até serem processados)
    public AnaliseEmocional? Analise { get; private set; }
    public SugestaoDua? SugestaoPedagogica { get; private set; }

    // Construtor vazio para o EF Core
    private RegistroAprendizagem() { }

    public RegistroAprendizagem(string alunoIdHash, string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException("O texto do relato não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(alunoIdHash))
            throw new ArgumentException("O ID do aluno é obrigatório.");

        AlunoIdHash = alunoIdHash;
        TextoOriginal = texto;
    }

    // Método Rico: A entidade sabe usar a estratégia para se preencher
    public void ProcessarAnalise(AnaliseEmocional analise, IDuaStrategy strategy)
    {
        this.Analise = analise;
        SugestaoPedagogica = strategy.GerarSugestao(this);
    }
}