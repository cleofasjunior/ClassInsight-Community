using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClassInsight.Application.UseCases.AnalisarRelato;
using ClassInsight.Application.UseCases.GerarTrilha;
using ClassInsight.Application.DTOs;
using System.Net.Mime;

namespace ClassInsight.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Operações de IA para análise pedagógica e inclusão (DUA).")]
public class InsightsController : ControllerBase
{
    private readonly IMediator _mediator;
    public InsightsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Realiza a análise emocional e pedagógica de um relato.
    /// </summary>
    /// <remarks>
    /// **Segurança:** Requer perfil 'Professor' ou 'Admin'. 
    /// O campo 'AlunoIdHash' deve conter apenas o identificador anônimo do estudante.
    /// </remarks>
    /// <param name="command">Dados do relato e identificador anônimo do aluno.</param>
    /// <returns>Retorna o sentimento detectado e a sugestão baseada no Desenho Universal para Aprendizagem.</returns>
    /// <response code="200">Análise concluída com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Usuário não possui permissão (Requer Professor/Admin).</response>
    [HttpPost("analisar")]
    [Authorize(Roles = "Professor, Admin")]
    public async Task<ActionResult<ResultadoAnaliseDto>> AnalisarRelato([FromBody] AnalisarRelatoCommand command)
    {
        var resultado = await _mediator.Send(command);
        return Ok(resultado);
    }

    /// <summary>
    /// Gera uma trilha de aprendizagem personalizada com IA Generativa.
    /// </summary>
    /// <remarks>
    /// Utiliza o histórico de registros do aluno (via RAG) para adaptar os recursos de ensino.
    /// </remarks>
    [HttpPost("trilha-personalizada")]
    [Authorize(Roles = "Professor, Admin")]
    public async Task<IActionResult> GerarTrilha([FromBody] GerarTrilhaCommand command)
    {
        var resultado = await _mediator.Send(command);
        return Ok(resultado);
    }
}