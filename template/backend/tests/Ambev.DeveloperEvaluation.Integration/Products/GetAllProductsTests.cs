using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Products;

/// <summary>
/// Integration tests for the GetAllProducts endpoint.
/// </summary>
public class GetAllProductsTests : IntegrationTestBase
{
    public GetAllProductsTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact(DisplayName = "GET /api/products - Should return all created products")]
    public async Task Given_ProductsExist_When_GettingAll_Then_ShouldReturnList()
    {
        // Arrange
        var request1 = new CreateProductRequest
        {
            Name = "Fone Bluetooth",
            Description = "Fone de ouvido sem fio",
            UnitPrice = 299.90m,
            AvailableQuantity = 80
        };

        var request2 = new CreateProductRequest
        {
            Name = "Webcam HD",
            Description = "Câmera 1080p para videoconferência",
            UnitPrice = 249.00m,
            AvailableQuantity = 35
        };

        await Client.PostAsJsonAsync("/api/products", request1);
        await Client.PostAsJsonAsync("/api/products", request2);

        // Act
        var response = await Client.GetAsync("/api/products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<IEnumerable<GetAllProductsResponse>>();
        products.Should().NotBeNull();
        products!.Should().Contain(p => p.Name == request1.Name);
        products.Should().Contain(p => p.Name == request2.Name);
    }
}
