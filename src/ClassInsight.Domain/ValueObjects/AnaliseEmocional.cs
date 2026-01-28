using ClassInsight.Domain.Enums;

namespace ClassInsight.Domain.ValueObjects;

public record AnaliseEmocional(
    TipoSentimento SentimentoGeral,
    double PontuacaoConfianca,
    List<string> PalavrasChave
);