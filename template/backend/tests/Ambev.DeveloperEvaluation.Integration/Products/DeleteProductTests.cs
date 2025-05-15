using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Products;

/// <summary>
/// Integration tests for the DeleteProduct endpoint.
/// </summary>
public class DeleteProductTests : IntegrationTestBase
{
    public DeleteProductTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact(DisplayName = "DELETE /api/products/{id} - Should delete product and return 200 OK")]
    public async Task Given_ExistingProduct_When_Deleting_Then_ShouldSucceed()
    {
        // Arrange - cria produto
        var createRequest = new CreateProductRequest
        {
            Name = "HD Externo",
            Description = "1TB USB 3.0",
            UnitPrice = 299.90m,
            AvailableQuantity = 15
        };

        var createResponse = await Client.PostAsJsonAsync("/api/products", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<CreateProductResponse>();
        var productId = created!.Id;

        // Act - deleta produto
        var deleteResponse = await Client.DeleteAsync($"/api/products/{productId}");

        // Assert - DELETE OK
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Act - tenta buscar produto novamente
        var getResponse = await Client.GetAsync($"/api/products/{productId}");

        // Assert - produto não encontrado
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
