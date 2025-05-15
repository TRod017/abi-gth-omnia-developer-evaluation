using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartHandler> _logger;
    private readonly UpdateCartHandler _handler;

    public UpdateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<UpdateCartHandler>>();
        _handler = new UpdateCartHandler(_cartRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid command When handling Then should update cart and return result")]
    public async Task Handle_ValidCommand_ReturnsUpdatedResult()
    {
        // Arrange
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        var cart = new Cart { Id = command.Id, UserId = command.UserId };
        var result = new UpdateCartResult { Id = cart.Id };

        _mapper.Map<Cart>(command).Returns(cart);
        _mapper.Map<UpdateCartResult>(cart).Returns(result);
        _cartRepository.UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(cart.Id);
        await _cartRepository.Received(1).UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }
}
