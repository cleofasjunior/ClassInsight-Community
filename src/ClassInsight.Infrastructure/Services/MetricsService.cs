using ClassInsight.Application.Interfaces;
using Microsoft.Extensions.Logging;
using AppInterfaces = ClassInsight.Application.Interfaces;

namespace ClassInsight.Infrastructure.Services;

public class ConsoleMetricsService : AppInterfaces.IMetricsService
{
    private readonly ILogger<ConsoleMetricsService> _logger;

    public ConsoleMetricsService(ILogger<ConsoleMetricsService> logger)
    {
        _logger = logger;
    }

    public void RegistrarEvento(string nomeEvento, Dictionary<string, string>? propriedades = null)
    {
        _logger.LogInformation("[METRICA] Evento: {Evento} | Dados: {Dados}", nomeEvento, propriedades);
    }
}