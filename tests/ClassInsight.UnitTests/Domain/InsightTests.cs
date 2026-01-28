using ClassInsight.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ClassInsight.UnitTests.Domain;

public class InsightTests
{
    [Fact]
    public void CriarInsight_ComDadosValidos_DeveProsperar()
    {
        // Arrange
        var alunoHash = "hash_teste_2026";
        var relato = "O aluno demonstrou progresso no DUA.";
        var disciplina = "Matemática";

        // Act
        // Se sua classe Insight tiver um construtor, passamos os parâmetros:
        var insight = new Insight(alunoHash, relato, disciplina);

        // Assert
        insight.AlunoIdHash.Should().Be(alunoHash);
        insight.TextoRelato.Should().Be(relato);
    }
}