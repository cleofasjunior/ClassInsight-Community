namespace ClassInsight.Domain.ValueObjects;

public record PassoTrilha(
    string Titulo, 
    string Descricao, 
    string TipoRecurso // Ex: "Video", "Podcast", "Jogo"
);