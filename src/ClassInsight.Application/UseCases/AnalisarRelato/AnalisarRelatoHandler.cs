using MediatR;
using ClassInsight.Domain.Entities;
using ClassInsight.Domain.Interfaces;
using ClassInsight.Application.DTOs;
using AppInterfaces = ClassInsight.Application.Interfaces;

namespace ClassInsight.Application.UseCases.AnalisarRelato;

public class AnalisarRelatoHandler : IRequestHandler<AnalisarRelatoCommand, ResultadoAnaliseDto>
{
    private readonly IAiService _aiService;
    private readonly IRegistroRepository _repository;
    private readonly IDuaStrategy _duaStrategy;
    
    // 💡 Usamos o apelido aqui para evitar o erro CS0104
    private readonly AppInterfaces.IMetricsService _metrics;

    public AnalisarRelatoHandler(
        IAiService aiService, 
        IRegistroRepository repository, 
        IDuaStrategy duaStrategy,
        AppInterfaces.IMetricsService metrics) // 💡 E aqui também
    {
        _aiService = aiService;
        _repository = repository;
        _duaStrategy = duaStrategy;
        _metrics = metrics;
    }

    public async Task<ResultadoAnaliseDto> Handle(AnalisarRelatoCommand request, CancellationToken cancellationToken)
    {
        // 1. Criar a Entidade (Domínio Rico)
        var registro = new RegistroAprendizagem(request.AlunoIdHash, request.TextoRelato);

        // 2. Chamar IA para Análise de Sentimento
        var analise = await _aiService.AnalisarTextoAsync(request.TextoRelato);

        // 3. Processar regras DUA
        registro.ProcessarAnalise(analise, _duaStrategy);

        // 4. Persistir
        await _repository.AdicionarAsync(registro);

        // 5. Registrar Métrica de Negócio
        _metrics.RegistrarEvento("AnaliseRealizada", new Dictionary<string, string> 
        { 
            { "Sentimento", analise.SentimentoGeral.ToString() } 
        });

        // 6. Retornar DTO (Com blindagem contra Nulos)
return new ResultadoAnaliseDto(
    registro.Id,
    registro.Analise?.SentimentoGeral.ToString() ?? "Indefinido",
    registro.Analise?.PontuacaoConfianca ?? 0,
    registro.SugestaoPedagogica?.DescricaoAcao ?? "Sem sugestão disponível",
    registro.SugestaoPedagogica?.FerramentaSugerida ?? "Nenhuma ferramenta"
);
    }
}