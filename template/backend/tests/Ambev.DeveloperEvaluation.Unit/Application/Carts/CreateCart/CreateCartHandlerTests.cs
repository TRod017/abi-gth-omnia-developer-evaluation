using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.CreateCart;

/// <summary>
/// Unit tests for the <see cref="CreateCartHandler"/> class.
/// Validates behavior when handling CreateCartCommand requests.
/// </summary>
public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateCartHandler>>();
        _handler = new CreateCartHandler(_cartRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid command When handling Then should create cart and return result")]
    public async Task Handle_ValidCommand_ReturnsCreatedResult()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();

        // Use CartTestData para gerar entidade
        var cart = CartTestData.GenerateValidCart();

        // Ajusta propriedades para refletir o comando
        cart.UserId = command.UserId;
        cart.Items = command.Items.Select(i => new Ambev.DeveloperEvaluation.Domain.Entities.CartItem
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity
        }).ToList();

        var expectedResult = new CreateCartResult { Id = cart.Id };

        _mapper.Map<Cart>(command).Returns(cart);
        _mapper.Map<CreateCartResult>(cart).Returns(expectedResult);
        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(cart.Id);
        await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid command When handling Then should map command to entity")]
    public async Task Handle_ValidCommand_MapsCommandToEntity()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();

        var cart = CartTestData.GenerateValidCart();

        cart.UserId = command.UserId;
        cart.Items = command.Items.Select(i => new Ambev.DeveloperEvaluation.Domain.Entities.CartItem
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity
        }).ToList();

        _mapper.Map<Cart>(command).Returns(cart);
        _cartRepository.CreateAsync(cart, Arg.Any<CancellationToken>()).Returns(cart);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapper.Received(1).Map<Cart>(command);
        await _cartRepository.Received(1).CreateAsync(cart, Arg.Any<CancellationToken>());
    }
}
