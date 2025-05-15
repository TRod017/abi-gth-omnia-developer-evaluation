using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the GetAllProductsHandler class.
/// </summary>
public class GetAllProductsHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllProductsHandler> _logger;
    private readonly GetAllProductsHandler _handler;

    public GetAllProductsHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetAllProductsHandler>>();
        _handler = new GetAllProductsHandler(_productRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "When handling request Then should return all mapped products")]
    public async Task Handle_WhenCalled_ReturnsAllProducts()
    {
        // Arrange
        var products = GetAllProductsHandlerTestData.GenerateProducts();
        var results = products.Select(p => new GetAllProductsResult
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            UnitPrice = p.UnitPrice,
            AvailableQuantity = p.AvailableQuantity
        });

        _productRepository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(products);
        _mapper.Map<IEnumerable<GetAllProductsResult>>(products).Returns(results);

        // Act
        var response = await _handler.Handle(new GetAllProductsCommand(), CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Count().Should().Be(products.Count);
        response.Select(r => r.Id).Should().BeEquivalentTo(products.Select(p => p.Id));
    }
}
