using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Products;

/// <summary>
/// Integration tests for the GetProduct endpoint.
/// </summary>
public class GetProductTests : IntegrationTestBase
{
    public GetProductTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact(DisplayName = "GET /api/products/{id} - Should return product data")]
    public async Task Given_ExistingProduct_When_GettingById_Then_ShouldReturnProduct()
    {
        // Arrange
        var createRequest = new CreateProductRequest
        {
            Name = "Mouse Gamer",
            Description = "Mouse com 6 botões programáveis",
            UnitPrice = 149.99m,
            AvailableQuantity = 100
        };

        var createResponse = await Client.PostAsJsonAsync("/api/products", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdProduct = await createResponse.Content.ReadFromJsonAsync<CreateProductResponse>();
        createdProduct.Should().NotBeNull();
        var productId = createdProduct!.Id;

        // Act
        var getResponse = await Client.GetAsync($"/api/products/{productId}");

        // Assert
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getProduct = await getResponse.Content.ReadFromJsonAsync<GetProductResponse>();
        getProduct.Should().NotBeNull();
        getProduct!.Id.Should().Be(productId);
        getProduct.Name.Should().Be(createRequest.Name);
    }
}
