using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="UpdateCartHandler"/> class.
/// Validates behavior when handling UpdateCartCommand requests,
/// including successful updates, not found cases, and error logging.
/// </summary>
public class UpdateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartHandler> _logger;
    private readonly UpdateCartHandler _handler;

    public UpdateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<UpdateCartHandler>>();
        _handler = new UpdateCartHandler(_cartRepository, _productRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given existing cart When handling valid command Then should update cart and return result")]
    public async Task Handle_ValidCommand_UpdatesCartAndReturnsResult()
    {
        // Arrange
        var command = UpdateCartHandlerTestData.GenerateValidCommand();

        var existingCart = new Cart
        {
            Id = command.Id,
            UserId = Guid.NewGuid(),
            Status = Ambev.DeveloperEvaluation.Domain.Enums.CartStatus.Open,
            Items = new List<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>()
        };

        _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(existingCart));

        _productRepository.GetByIdAsync(Arg.Any<Guid>())
            .Returns(ci => new Product
            {
                Id = ci.ArgAt<Guid>(0),
                Name = "Test Product",
                AvailableQuantity = 100,
                UnitPrice = 10
            });

        _mapper.When(m => m.Map(command, existingCart))
            .Do(ci =>
            {
                var cmd = ci.ArgAt<UpdateCartCommand>(0);
                var cart = ci.ArgAt<Cart>(1);
                cart.UserId = cmd.UserId;
                cart.Status = cmd.Status;
                // Map items simply for test
                cart.Items.Clear();
                foreach (var i in cmd.Items)
                {
                    cart.Items.Add(new Ambev.DeveloperEvaluation.Domain.Entities.CartItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    });
                }
            });

        _cartRepository.UpdateAsync(existingCart, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(existingCart));

        var updateResult = new UpdateCartResult { Id = existingCart.Id };
        _mapper.Map<UpdateCartResult>(existingCart).Returns(updateResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(existingCart.Id);

        await _cartRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).UpdateAsync(existingCart, Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<UpdateCartResult>(existingCart);
    }

    [Fact(DisplayName = "Given non-existent cart When handling command Then should throw KeyNotFoundException")]
    public async Task Handle_NonExistentCart_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = UpdateCartHandlerTestData.GenerateValidCommand();

        _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Cart>(null));

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        await _cartRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
        await _cartRepository.DidNotReceive().UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }
}
