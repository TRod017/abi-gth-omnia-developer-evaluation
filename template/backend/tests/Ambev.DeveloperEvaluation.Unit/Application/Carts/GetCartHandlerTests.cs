using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class GetCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartHandler> _logger;
    private readonly GetCartHandler _handler;

    public GetCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetCartHandler>>();
        _handler = new GetCartHandler(_cartRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid command When handling Then should return cart")]
    public async Task Handle_ValidCommand_ReturnsCart()
    {
        // Arrange
        var query = GetCartHandlerTestData.GenerateValidQuery();
        var cart = new Cart { Id = query.Id };
        var result = new GetCartResult { Id = query.Id };

        _cartRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>()).Returns(cart);
        _mapper.Map<GetCartResult>(cart).Returns(result);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(query.Id);
    }
}
