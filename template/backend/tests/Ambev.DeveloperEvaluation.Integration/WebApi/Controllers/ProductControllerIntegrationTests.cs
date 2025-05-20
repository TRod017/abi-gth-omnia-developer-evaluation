using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.Integration.Common;
using Xunit;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Integration.WebApi.Controllers;

/// <summary>
/// Integration tests for the ProductsController.
/// Validates the complete HTTP pipeline from request to response,
/// using in-memory database and fake authentication.
/// </summary>
public class ProductControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes the test client using the custom test server.
    /// </summary>
    public ProductControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "fake-jwt-token");
    }

    [Fact(DisplayName = "POST /api/products should return 201 Created")]
    public async Task Post_ShouldCreateProduct()
    {
        var request = new
        {
            Name = "Integration Cerveja",
            Description = "Cerveja artesanal leve",
            UnitPrice = 5.99m,
            AvailableQuantity = 100
        };

        var response = await _client.PostAsJsonAsync("/api/products", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Product created successfully").And.Contain("id");
    }

    [Fact(DisplayName = "GET /api/products should return 200 OK with products list")]
    public async Task GetAll_ShouldReturnProducts()
    {
        var response = await _client.GetAsync("/api/products?_page=1&_size=10");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("Products retrieved successfully");
    }

    [Fact(DisplayName = "GET /api/products/{id} should return 200 OK for existing product")]
    public async Task GetById_ShouldReturnProduct()
    {
        var createRequest = new
        {
            Name = "Produto Unico",
            Description = "Produto criado para teste de GET",
            UnitPrice = 4.50m,
            AvailableQuantity = 20
        };

        var postResponse = await _client.PostAsJsonAsync("/api/products", createRequest);
        var postJson = await postResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(postJson);
        var id = doc.RootElement.GetProperty("data").GetProperty("id").GetString();

        var getResponse = await _client.GetAsync($"/api/products/{id}");

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getJson = await getResponse.Content.ReadAsStringAsync();
        getJson.Should().Contain("Product retrieved successfully");
    }

    [Fact(DisplayName = "PUT /api/products/{id} should update product and return 200 OK")]
    public async Task Put_ShouldUpdateProduct()
    {
        var createRequest = new
        {
            Name = "Produto Atualizado",
            Description = "Para ser atualizado",
            UnitPrice = 2.99m,
            AvailableQuantity = 10
        };

        var postResponse = await _client.PostAsJsonAsync("/api/products", createRequest);
        var postJson = await postResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(postJson);
        var id = doc.RootElement.GetProperty("data").GetProperty("id").GetString();

        var updateRequest = new
        {
            Id = id,
            Name = "Produto Atualizado Final",
            Description = "Descrição atualizada",
            UnitPrice = 3.49m,
            AvailableQuantity = 30
        };

        var putResponse = await _client.PutAsJsonAsync($"/api/products/{id}", updateRequest);

        putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var putJson = await putResponse.Content.ReadAsStringAsync();
        putJson.Should().Contain("Product updated successfully");
    }

    [Fact(DisplayName = "DELETE /api/products/{id} should remove product and return 200 OK")]
    public async Task Delete_ShouldRemoveProduct()
    {
        var createRequest = new
        {
            Name = "Produto para Deletar",
            Description = "Será deletado",
            UnitPrice = 1.00m,
            AvailableQuantity = 5
        };

        var postResponse = await _client.PostAsJsonAsync("/api/products", createRequest);
        var postJson = await postResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(postJson);
        var id = doc.RootElement.GetProperty("data").GetProperty("id").GetString();

        var deleteResponse = await _client.DeleteAsync($"/api/products/{id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await deleteResponse.Content.ReadAsStringAsync();
        json.Should().Contain("Product deleted successfully");
    }
}
