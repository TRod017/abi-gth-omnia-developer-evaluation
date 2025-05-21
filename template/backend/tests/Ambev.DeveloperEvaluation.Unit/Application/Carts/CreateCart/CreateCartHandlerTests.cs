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

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="CreateCartHandler"/> class.
/// Validates behavior when handling CreateCartCommand requests.
/// </summary>
public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateCartHandler>>();
        _handler = new CreateCartHandler(_cartRepository, _productRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid command When handling Then should create cart and return result")]
    public async Task Handle_ValidCommand_ReturnsCreatedResult()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var product = ProductTestData.GenerateValidProduct();
        var cart = CartTestData.GenerateValidCart();

        // Configura o retorno do repositório de produto para cada ProductId do comando
        foreach (var item in command.Items)
        {
            _productRepository.GetByIdAsync(item.ProductId).Returns(product);
        }

        var expectedResult = new CreateCartResult { Id = cart.Id };

        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);
        _mapper.Map<CreateCartResult>(Arg.Any<Cart>()).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(cart.Id);
        await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid command When handling Then should map command to entity")]
    public async Task Handle_ValidCommand_MapsCommandToEntity()
    {
        // Arrange
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var product = ProductTestData.GenerateValidProduct();
        var cart = CartTestData.GenerateValidCart();

        foreach (var item in command.Items)
        {
            _productRepository.GetByIdAsync(item.ProductId).Returns(product);
        }

        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);
        _mapper.Map<CreateCartResult>(Arg.Any<Cart>()).Returns(new CreateCartResult { Id = cart.Id });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _productRepository.Received(command.Items.Count).GetByIdAsync(Arg.Any<Guid>());
        await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }
}
