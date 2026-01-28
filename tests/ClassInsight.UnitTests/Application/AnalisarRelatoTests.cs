using ClassInsight.Application.UseCases.AnalisarRelato;
using ClassInsight.Application.DTOs;
using ClassInsight.Domain.Interfaces;
using ClassInsight.Domain.Entities;
using ClassInsight.Domain.Enums;        
using ClassInsight.Domain.ValueObjects; 
using Moq;
using FluentAssertions;
using Xunit;
using AppInterfaces = ClassInsight.Application.Interfaces;

namespace ClassInsight.UnitTests.Application;

public class AnalisarRelatoTests
{
    private readonly Mock<IAiService> _aiServiceMock;
    private readonly Mock<IRegistroRepository> _repositoryMock;
    private readonly Mock<IDuaStrategy> _duaStrategyMock;
    private readonly Mock<AppInterfaces.IMetricsService> _metricsMock;
    private readonly AnalisarRelatoHandler _handler;

    public AnalisarRelatoTests()
    {
        _aiServiceMock = new Mock<IAiService>();
        _repositoryMock = new Mock<IRegistroRepository>();
        _duaStrategyMock = new Mock<IDuaStrategy>();
        _metricsMock = new Mock<AppInterfaces.IMetricsService>();

        _handler = new AnalisarRelatoHandler(
            _aiServiceMock.Object, 
            _repositoryMock.Object, 
            _duaStrategyMock.Object, 
            _metricsMock.Object);
    }

    [Fact]
    public async Task Handle_DeveProcessarFluxoCompleto_ComSucesso()
    {
        // 1. Arrange
        var request = new AnalisarRelatoCommand { 
            AlunoIdHash = "hash_anonimo_2026", 
            TextoRelato = "O aluno participou ativamente da aula de inclusão." 
        };

        // Usando seus tipos reais: AnaliseEmocional e TipoSentimento
        var analiseFake = new AnaliseEmocional(
            TipoSentimento.Positivo, 
            0.95, 
            new List<string> { "participação", "inclusão" }
        );
        
        _aiServiceMock.Setup(x => x.AnalisarTextoAsync(It.IsAny<string>()))
                      .ReturnsAsync(analiseFake);

        // 2. Act
        var resultado = await _handler.Handle(request, CancellationToken.None);

        // 3. Assert
        resultado.Should().NotBeNull();
        // Verificamos se o seu Handler converteu o Enum para String corretamente no DTO
        resultado.Sentimento.Should().Be("Positivo");
        
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<RegistroAprendizagem>()), Times.Once);
        _metricsMock.Verify(m => m.RegistrarEvento(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
    }
}