using ClassInsight.Domain.Interfaces;
using ClassInsight.Domain.ValueObjects;
using ClassInsight.Domain.Enums;

namespace ClassInsight.Infrastructure.Services.Fakes;

// Simulação GRATUITA da IA Analítica
public class FakeAiService : IAiService
{
    public Task<AnaliseEmocional> AnalisarTextoAsync(string texto)
    {
        // Lógica simples baseada em palavras-chave para teste
        var sentimento = texto.ToLower().Contains("odeio") || texto.ToLower().Contains("difícil") 
            ? TipoSentimento.Negativo 
            : TipoSentimento.Positivo;

        var topicos = new List<string>();
        if (texto.ToLower().Contains("escrever")) topicos.Add("escrever");
        if (texto.ToLower().Contains("medo")) topicos.Add("medo");

        return Task.FromResult(new AnaliseEmocional(sentimento, 0.99, topicos));
    }
}