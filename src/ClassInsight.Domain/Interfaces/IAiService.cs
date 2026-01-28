using ClassInsight.Domain.ValueObjects;

namespace ClassInsight.Domain.Interfaces;

public interface IAiService
{
    // Analisa sentimento e extrai palavras-chave
    Task<AnaliseEmocional> AnalisarTextoAsync(string texto);
}