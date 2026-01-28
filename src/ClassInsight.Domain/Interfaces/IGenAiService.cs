using ClassInsight.Domain.ValueObjects;

namespace ClassInsight.Domain.Interfaces;

public interface IGenAiService
{
    // Gera a trilha personalizada usando OpenAI
    Task<List<PassoTrilha>> GerarTrilhaPersonalizadaAsync(
        string topico, 
        string perfilAluno, 
        string dificuldadeIdentificada
    );
}