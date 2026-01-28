using FluentValidation;

namespace ClassInsight.Application.UseCases.AnalisarRelato;

public class AnalisarRelatoValidator : AbstractValidator<AnalisarRelatoCommand>
{
    public AnalisarRelatoValidator()
    {
        RuleFor(x => x.AlunoIdHash)
            .NotEmpty().WithMessage("O ID do aluno é obrigatório.");

        RuleFor(x => x.TextoRelato)
            .NotEmpty().WithMessage("O texto do relato não pode ser vazio.")
            .MinimumLength(5).WithMessage("O relato deve ter pelo menos 5 caracteres.");
    }
}