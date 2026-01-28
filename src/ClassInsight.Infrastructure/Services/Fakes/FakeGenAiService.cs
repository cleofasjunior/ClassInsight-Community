using ClassInsight.Domain.Interfaces;
using ClassInsight.Domain.ValueObjects;

namespace ClassInsight.Infrastructure.Services.Fakes;

// Simulação GRATUITA do GPT
public class FakeGenAiService : IGenAiService
{
    public Task<List<PassoTrilha>> GerarTrilhaPersonalizadaAsync(string topico, string perfil, string dificuldade)
    {
        var trilhaFake = new List<PassoTrilha>
        {
            new PassoTrilha("Passo 1 (Fake)", $"Introdução ao {topico} para perfil {perfil}", "Vídeo"),
            new PassoTrilha("Passo 2 (Fake)", "Exercício Prático", "Jogo"),
            new PassoTrilha("Passo 3 (Fake)", "Avaliação Oral", "Podcast")
        };

        return Task.FromResult(trilhaFake);
    }
}