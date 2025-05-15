using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Products;

/// <summary>
/// Integration tests for the CreateProduct endpoint.
/// </summary>
public class CreateProductTests : IntegrationTestBase
{
    public CreateProductTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact(DisplayName = "POST /api/products - Should create product and return 201 Created")]
    public async Task Given_ValidProductRequest_When_Posting_Then_ShouldReturnCreated()
    {
        // Arrange
        var request = new CreateProductRequest
        {
            Name = "Teclado Mecânico",
            Description = "Teclado RGB para gamers",
            UnitPrice = 399.99m,
            AvailableQuantity = 50
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/products", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var result = await response.Content.ReadFromJsonAsync<CreateProductResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().NotBeEmpty();
    }
}
