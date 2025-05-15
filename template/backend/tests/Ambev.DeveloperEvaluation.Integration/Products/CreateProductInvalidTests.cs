using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Products;

/// <summary>
/// Integration tests for invalid CreateProduct requests.
/// </summary>
public class CreateProductInvalidTests : IntegrationTestBase
{
    public CreateProductInvalidTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Theory(DisplayName = "POST /api/products - Should return 400 when data is invalid")]
    [InlineData("", "Descrição válida", 100.0, 10)]            // Nome vazio
    [InlineData("Produto", "", 100.0, 10)]                      // Descrição vazia
    [InlineData("Produto", "Descrição", -10.0, 10)]            // Preço negativo
    [InlineData("Produto", "Descrição", 100.0, -5)]            // Quantidade negativa
    public async Task Given_InvalidRequest_When_Posting_Then_ShouldReturnBadRequest(
        string name, string description, decimal price, int quantity)
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = name,
            Description = description,
            UnitPrice = price,
            AvailableQuantity = quantity
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/products", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
