using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Products;

/// <summary>
/// Integration tests for updating a product with an invalid or non-existent ID.
/// </summary>
public class UpdateProductInvalidTests : IntegrationTestBase
{
    public UpdateProductInvalidTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact(DisplayName = "PUT /api/products/{id} - Should return 404 when product does not exist")]
    public async Task Given_NonExistentProductId_When_Updating_Then_ShouldReturnNotFound()
    {
        // Arrange
        var invalidId = Guid.NewGuid(); // ID que não existe
        var request = new UpdateProductRequest
        {
            Id = invalidId,
            Name = "Produto Atualizado",
            Description = "Nova descrição",
            UnitPrice = 500.00m,
            AvailableQuantity = 5
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/products/{invalidId}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
