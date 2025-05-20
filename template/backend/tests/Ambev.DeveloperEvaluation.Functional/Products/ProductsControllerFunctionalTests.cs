using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.Functional.Common;
using Xunit;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Functional.Products;

/// <summary>
/// Functional tests for the /api/products endpoints.
/// Validates creation, retrieval, updating and deletion of products.
/// </summary>
public class ProductsControllerFunctionalTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes the test client with fake authentication.
    /// </summary>
    public ProductsControllerFunctionalTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fake-jwt-token");
    }

    [Fact(DisplayName = "POST /api/products should return 201 Created when valid")]
    public async Task CreateProduct_ShouldReturnCreated()
    {
        var request = new
        {
            Name = "Skol Pilsen 350ml",
            Description = "Cerveja Pilsen leve e refrescante",
            UnitPrice = 2.79m,
            AvailableQuantity = 1000
        };

        var response = await _client.PostAsJsonAsync("/api/products", request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("Product created successfully").And.Contain("id");
    }

    [Fact(DisplayName = "GET /api/products should return 200 OK with list")]
    public async Task GetAllProducts_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/products?_page=1&_size=10");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("Products retrieved successfully");
    }

    [Fact(DisplayName = "GET /api/products/{id} should return 200 OK when product exists")]
    public async Task GetProductById_ShouldReturnOk()
    {
        var createRequest = new
        {
            Name = "Skol Pilsen 350ml",
            Description = "Cerveja Pilsen leve e refrescante",
            UnitPrice = 2.79m,
            AvailableQuantity = 1000
        };

        var postResponse = await _client.PostAsJsonAsync("/api/products", createRequest);
        var postJson = await postResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(postJson);
        string id = doc.RootElement.GetProperty("data").GetProperty("id").GetString()!;

        var getResponse = await _client.GetAsync($"/api/products/{id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getJson = await getResponse.Content.ReadAsStringAsync();
        getJson.Should().Contain("Product retrieved successfully");
    }

    [Fact(DisplayName = "PUT /api/products/{id} should return 200 OK when product updated")]
    public async Task UpdateProduct_ShouldReturnOk()
    {
        // Arrange – cria produto com estrutura válida
        var createRequest = new
        {
            Name = "Skol Pilsen 350ml",
            Description = "Cerveja Pilsen leve e refrescante",
            UnitPrice = 2.79m,
            AvailableQuantity = 9
        };

        var postResponse = await _client.PostAsJsonAsync("/api/products", createRequest);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var postJson = await postResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(postJson);
        string id = doc.RootElement.GetProperty("data").GetProperty("id").GetString()!;

        // Act – envia PUT com os campos esperados pela API
        var updateRequest = new
        {
            Id = id,
            Name = "Product Updated",
            Description = "Updated description",
            UnitPrice = 4.5m,
            AvailableQuantity = 50
        };

        var putResponse = await _client.PutAsJsonAsync($"/api/products/{id}", updateRequest);

        // Assert
        putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var putJson = await putResponse.Content.ReadAsStringAsync();
        putJson.Should().Contain("Product updated successfully").And.Contain("id");
    }

    [Fact(DisplayName = "DELETE /api/products/{id} should return 200 OK when product deleted")]
    public async Task DeleteProduct_ShouldReturnOk()
    {
        // Arrange – cria um produto válido no banco in-memory
        var createRequest = new
        {
            Name = "Product to Delete",
            Description = "Will be deleted",
            UnitPrice = 1.0m,
            AvailableQuantity = 10
        };

        var postResponse = await _client.PostAsJsonAsync("/api/products", createRequest);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var postJson = await postResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(postJson);
        string id = doc.RootElement.GetProperty("data").GetProperty("id").GetString()!;

        // Act – executa o DELETE
        var deleteResponse = await _client.DeleteAsync($"/api/products/{id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var deleteJson = await deleteResponse.Content.ReadAsStringAsync();
        deleteJson.Should().Contain("Product deleted successfully");
    }
}
