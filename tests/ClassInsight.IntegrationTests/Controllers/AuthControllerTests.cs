using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using ClassInsight.API;

namespace ClassInsight.IntegrationTests.Controllers;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_ComCredenciaisValidas_DeveRetornarOkEToken()
    {
        // Arrange
        var loginRequest = new { Email = "prof@escola.com", Password = "123456" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Auth/login", loginRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("token");
    }
}