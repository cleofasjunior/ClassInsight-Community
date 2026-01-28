using MediatR;
using ClassInsight.Domain.Entities;

namespace ClassInsight.Application.UseCases.GerarTrilha;

// Retorna a própria Entidade TrilhaPedagogica (ou poderia ser um DTO complexo)
public record GerarTrilhaCommand(string AlunoIdHash, string TopicoDesejado) : IRequest<TrilhaPedagogica>;