using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Products;

/// <summary>
/// Integration tests for the UpdateProduct endpoint.
/// </summary>
public class UpdateProductTests : IntegrationTestBase
{
    public UpdateProductTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact(DisplayName = "PUT /api/products/{id} - Should update product and return 200 OK")]
    public async Task Given_ExistingProduct_When_Updating_Then_ShouldReturnSuccess()
    {
        // Arrange - cria produto original
        var createRequest = new CreateProductRequest
        {
            Name = "Monitor Full HD",
            Description = "Monitor 24'' HDMI",
            UnitPrice = 799.00m,
            AvailableQuantity = 10
        };

        var createResponse = await Client.PostAsJsonAsync("/api/products", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<CreateProductResponse>();
        var productId = created!.Id;

        // Act - atualiza produto
        var updateRequest = new UpdateProductRequest
        {
            Id = productId,
            Name = "Monitor 4K",
            Description = "Monitor 27'' com resolução UHD",
            UnitPrice = 1599.90m,
            AvailableQuantity = 20
        };

        var updateResponse = await Client.PutAsJsonAsync($"/api/products/{productId}", updateRequest);

        // Assert - PUT OK
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert - dados atualizados
        var getResponse = await Client.GetAsync($"/api/products/{productId}");
        var updatedProduct = await getResponse.Content.ReadFromJsonAsync<GetProductResponse>();

        updatedProduct.Should().NotBeNull();
        updatedProduct!.Name.Should().Be(updateRequest.Name);
        updatedProduct.Description.Should().Be(updateRequest.Description);
        updatedProduct.UnitPrice.Should().Be(updateRequest.UnitPrice);
        updatedProduct.AvailableQuantity.Should().Be(updateRequest.AvailableQuantity);
    }
}
