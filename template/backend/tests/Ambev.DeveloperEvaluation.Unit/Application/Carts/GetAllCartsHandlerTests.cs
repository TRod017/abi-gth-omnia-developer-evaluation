using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the GetAllCartsHandler class.
/// </summary>
public class GetAllCartsHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCartsHandler> _logger;
    private readonly GetAllCartsHandler _handler;

    public GetAllCartsHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetAllCartsHandler>>();
        _handler = new GetAllCartsHandler(_cartRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "When handling request Then should return all mapped carts")]
    public async Task Handle_WhenCalled_ReturnsAllCarts()
    {
        // Arrange
        var carts = CartHandlerTestData.GenerateCarts();
        var results = carts.Select(c => new GetAllCartsResult
        {
            Id = c.Id,
            UserId = c.UserId,
            Status = c.Status,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        });

        _cartRepository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(carts);
        _mapper.Map<IEnumerable<GetAllCartsResult>>(carts).Returns(results);

        // Act
        var response = await _handler.Handle(new GetAllCartsCommand(), CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Count().Should().Be(carts.Count);
        response.Select(r => r.Id).Should().BeEquivalentTo(carts.Select(c => c.Id));
    }
}
