using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.FunctionalTests.Common;
using Xunit;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.FunctionalTests.Auth;

/// <summary>
/// Functional tests for the authentication endpoint (/api/auth).
/// </summary>
public class AuthControllerFunctionalTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthControllerFunctionalTests"/> class
    /// using the provided <see cref="CustomWebApplicationFactory{TProgram}"/>.
    /// </summary>
    /// <param name="factory">The custom application factory for functional testing.</param>
    public AuthControllerFunctionalTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Tests that a valid login request returns 200 OK and includes a token in the response.
    /// </summary>
    [Fact(DisplayName = "POST /api/auth should return 200 OK with valid credentials")]
    public async Task PostAuth_ShouldReturnOk_WhenCredentialsAreValid()
    {
        // Arrange
        var loginRequest = new
        {
            Email = "teste@gmail.com",
            Password = "123456"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth", loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("token") // Ajuste conforme propriedade real do response
            .And.Contain("User authenticated successfully");
    }
}
