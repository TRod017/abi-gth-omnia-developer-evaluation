using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly ILogger<DeleteCartHandler> _logger;
    private readonly DeleteCartHandler _handler;

    public DeleteCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _logger = Substitute.For<ILogger<DeleteCartHandler>>();
        _handler = new DeleteCartHandler(_cartRepository, _logger);
    }

    [Fact(DisplayName = "Given valid command When handling Then should delete cart")]
    public async Task Handle_ValidCommand_DeletesCart()
    {
        // Arrange
        var command = DeleteCartHandlerTestData.GenerateValidCommand();

        _cartRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _cartRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }
}
