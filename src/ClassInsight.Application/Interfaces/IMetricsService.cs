namespace ClassInsight.Application.Interfaces;

public interface IMetricsService
{
    void RegistrarEvento(string nomeEvento, Dictionary<string, string>? propriedades = null);
}