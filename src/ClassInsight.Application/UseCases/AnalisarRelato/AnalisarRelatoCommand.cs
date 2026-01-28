using MediatR;
using ClassInsight.Application.DTOs;

namespace ClassInsight.Application.UseCases.AnalisarRelato;

// Este comando retorna um ResultadoAnaliseDto
public class AnalisarRelatoCommand : IRequest<ResultadoAnaliseDto>
{
    public string AlunoIdHash { get; set; } = string.Empty;
    public string TextoRelato { get; set; } = string.Empty;
}