using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Products;

/// <summary>
/// Integration tests for deleting a product twice.
/// </summary>
public class DeleteProductInvalidTests : IntegrationTestBase
{
    public DeleteProductInvalidTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact(DisplayName = "DELETE /api/products/{id} - Should return 404 if product already deleted")]
    public async Task Given_DeletedProduct_When_DeletingAgain_Then_ShouldReturnNotFound()
    {
        // Arrange - cria um produto
        var createRequest = new CreateProductRequest
        {
            Name = "Cabo USB-C",
            Description = "1 metro, carga rápida",
            UnitPrice = 39.90m,
            AvailableQuantity = 100
        };

        var createResponse = await Client.PostAsJsonAsync("/api/products", createRequest);
        var createdProduct = await createResponse.Content.ReadFromJsonAsync<CreateProductResponse>();
        var productId = createdProduct!.Id;

        // Act - deleta uma vez
        var firstDelete = await Client.DeleteAsync($"/api/products/{productId}");
        firstDelete.StatusCode.Should().Be(HttpStatusCode.OK);

        // Act - tenta deletar de novo
        var secondDelete = await Client.DeleteAsync($"/api/products/{productId}");

        // Assert - deve retornar NotFound
        secondDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
