using Azure;
using Azure.AI.TextAnalytics;
using ClassInsight.Domain.Enums;
using ClassInsight.Domain.Interfaces;
using ClassInsight.Domain.ValueObjects;
using ClassInsight.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace ClassInsight.Infrastructure.Services;

public class AzureAiService : IAiService
{
    private readonly TextAnalyticsClient _client;

    public AzureAiService(IOptions<AzureAiSettings> settings)
    {
        _client = new TextAnalyticsClient(
            new Uri(settings.Value.Endpoint), 
            new AzureKeyCredential(settings.Value.ApiKey));
    }

    public async Task<AnaliseEmocional> AnalisarTextoAsync(string texto)
    {
       
        var response = await _client.AnalyzeSentimentAsync(texto);
        var doc = response.Value;

        // Mapeia o sentimento da Azure para o nosso Enum (Domain)
        var sentimento = doc.Sentiment switch
        {
            TextSentiment.Positive => TipoSentimento.Positivo,
            TextSentiment.Negative => TipoSentimento.Negativo,
            TextSentiment.Mixed => TipoSentimento.Misto,
            _ => TipoSentimento.Neutro
        };

        // Se a Azure não minerou opiniões, devolvemos uma lista vazia por enquanto
        // (Isso não afeta o tutorial, pois usaremos o Fake na maior parte do tempo)
        var topicos = new List<string>();

        return new AnaliseEmocional(sentimento, doc.ConfidenceScores.Positive, topicos);
    }
}