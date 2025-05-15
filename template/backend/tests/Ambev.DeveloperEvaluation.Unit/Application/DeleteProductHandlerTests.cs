using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the DeleteProductHandler class.
/// </summary>
public class DeleteProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly DeleteProductHandler _handler;

    public DeleteProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new DeleteProductHandler(_productRepository);
    }

    [Fact(DisplayName = "Given valid command When deleting product Then should return true")]
    public async Task Handle_ValidCommand_ReturnsTrue()
    {
        // Arrange
        var command = DeleteProductHandlerTestData.GenerateValidCommand();
        _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        await _productRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid command When deleting product Then should throw validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Arrange
        var command = new DeleteProductCommand(); // ID vazio

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given product not found When deleting Then should return false")]
    public async Task Handle_NonExistingProduct_ReturnsFalse()
    {
        // Arrange
        var command = DeleteProductHandlerTestData.GenerateValidCommand();
        _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }
}
