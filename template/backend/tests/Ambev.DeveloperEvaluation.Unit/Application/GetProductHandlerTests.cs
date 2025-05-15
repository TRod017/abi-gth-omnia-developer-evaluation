using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
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
/// Contains unit tests for the GetProductHandler class.
/// </summary>
public class GetProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductHandler> _logger;
    private readonly GetProductHandler _handler;

    public GetProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetProductHandler>>();
        _handler = new GetProductHandler(_productRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid command When handling Then should return product data")]
    public async Task Handle_ValidCommand_ReturnsProduct()
    {
        // Arrange
        var command = GetProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = command.Id,
            Name = "Mouse",
            Description = "Wireless Mouse",
            UnitPrice = 99.90m,
            AvailableQuantity = 20
        };

        var result = new GetProductResult
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            UnitPrice = product.UnitPrice,
            AvailableQuantity = product.AvailableQuantity
        };

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(product);
        _mapper.Map<GetProductResult>(product).Returns(result);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(command.Id);
        response.Name.Should().Be(product.Name);
    }

    [Fact(DisplayName = "Given invalid command When handling Then should throw validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Arrange
        var command = new GetProductCommand(); // invalid (empty ID)

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given non-existent product When handling Then should throw not found exception")]
    public async Task Handle_NonExistingProduct_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = GetProductHandlerTestData.GenerateValidCommand();
        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Product?)null);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
