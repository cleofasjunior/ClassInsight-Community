namespace ClassInsight.Application.DTOs;

public record ResultadoAnaliseDto(
    Guid Id,
    string Sentimento,
    double Confianca,
    string SugestaoPedagogica,
    string FerramentaSugerida
);