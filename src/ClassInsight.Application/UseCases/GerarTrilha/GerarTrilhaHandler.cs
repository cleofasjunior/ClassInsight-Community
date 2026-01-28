using MediatR;
using ClassInsight.Domain.Entities;
using ClassInsight.Domain.Interfaces;
using ClassInsight.Domain.Enums;
using AppInterfaces = ClassInsight.Application.Interfaces;

namespace ClassInsight.Application.UseCases.GerarTrilha;
public class GerarTrilhaHandler : IRequestHandler<GerarTrilhaCommand, TrilhaPedagogica>
{
    private readonly IRegistroRepository _repository;
    private readonly IGenAiService _genAiService;
    
    private readonly AppInterfaces.IMetricsService _metrics;

    public GerarTrilhaHandler(
        IRegistroRepository repository, 
        IGenAiService genAiService, 
        AppInterfaces.IMetricsService metrics)
    {
        _repository = repository;
        _genAiService = genAiService;
        _metrics = metrics;
    }

    public async Task<TrilhaPedagogica> Handle(GerarTrilhaCommand request, CancellationToken cancellationToken)
    {
        // 1. RAG Simplificado: Buscar histórico do aluno
        var historico = await _repository.ObterUltimosRegistrosAsync(request.AlunoIdHash, 5);

        // 2. Montar perfil básico com base nos dados
        string perfil = "Aluno Padrão";
        string dificuldade = "Nenhuma detectada";

        if (historico.Any())
        {
            var negativos = historico.Count(h => h.Analise?.SentimentoGeral == TipoSentimento.Negativo);
            if (negativos > 2) dificuldade = "Frustração Recorrente";
        }

        // 3. Gerar Trilha com OpenAI
    
        var passos = await _genAiService.GerarTrilhaPersonalizadaAsync(
            request.TopicoDesejado, 
            perfil, 
            dificuldade
        );

        // 4. Criar Entidade e Retornar
        var trilha = new TrilhaPedagogica(request.AlunoIdHash, request.TopicoDesejado);
        trilha.AdicionarPassos(passos);

        _metrics.RegistrarEvento("TrilhaGerada", new Dictionary<string, string> { { "Topico", request.TopicoDesejado } });

        return trilha;
    }
}