using ClassInsight.Domain.Enums;

namespace ClassInsight.Domain.ValueObjects;

public record SugestaoDua(
    string Titulo,
    string DescricaoAcao,
    PrincipioDua PrincipioFocado,
    string FerramentaSugerida
);