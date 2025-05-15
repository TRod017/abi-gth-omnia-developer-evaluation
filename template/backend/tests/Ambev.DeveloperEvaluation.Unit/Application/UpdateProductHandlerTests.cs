using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the UpdateProductHandler class.
/// </summary>
public class UpdateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UpdateProductHandler _handler;

    public UpdateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateProductHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When handling Then should update product and return result")]
    public async Task Handle_ValidCommand_ReturnsUpdatedResult()
    {
        // Arrange
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        var existingProduct = new Product
        {
            Id = command.Id,
            Name = "Old Name",
            Description = "Old Desc",
            UnitPrice = 100,
            AvailableQuantity = 1
        };

        var updatedProduct = new Product
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            UnitPrice = command.UnitPrice,
            AvailableQuantity = command.AvailableQuantity
        };

        var result = new UpdateProductResult { Id = updatedProduct.Id };

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingProduct);
        _mapper.Map<Product>(command).Returns(updatedProduct);
        _productRepository.UpdateAsync(updatedProduct, Arg.Any<CancellationToken>()).Returns(updatedProduct);
        _mapper.Map<UpdateProductResult>(updatedProduct).Returns(result);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(updatedProduct.Id);
        await _productRepository.Received(1).UpdateAsync(updatedProduct, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid command When handling Then should throw validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Arrange
        var command = new UpdateProductCommand(); // invalid

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given non-existing product When handling Then should throw not found exception")]
    public async Task Handle_NonExistingProduct_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Product?)null);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact(DisplayName = "Given valid command When handling Then should map command to entity")]
    public async Task Handle_ValidCommand_MapsCommandToEntity()
    {
        // Arrange
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        var existingProduct = new Product { Id = command.Id };

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingProduct);
        _mapper.Map<Product>(command).Returns(existingProduct);
        _productRepository.UpdateAsync(existingProduct, Arg.Any<CancellationToken>()).Returns(existingProduct);
        _mapper.Map<UpdateProductResult>(existingProduct).Returns(new UpdateProductResult { Id = command.Id });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapper.Received(1).Map<Product>(command);
    }
}
