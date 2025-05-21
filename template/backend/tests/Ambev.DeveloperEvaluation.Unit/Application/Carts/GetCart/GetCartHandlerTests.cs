using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="GetCartHandler"/> class.
/// Validates behavior when handling <see cref="GetCartCommand"/> requests.
/// </summary>
public class GetCartHandlerTests
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartHandler> _logger;
    private readonly GetCartHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartHandlerTests"/> class.
    /// Sets up mocks for repository, mapper, and logger, and initializes the handler instance.
    /// </summary>
    public GetCartHandlerTests()
    {
        _repository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetCartHandler>>();
        _handler = new GetCartHandler(_repository, _mapper, _logger);
    }

    /// <summary>
    /// Tests that given a valid command and existing cart, the handler returns the mapped cart result.
    /// </summary>
    [Fact(DisplayName = "Given valid command with existing cart When handled Then returns mapped cart result")]
    public async Task Handle_ValidCommand_ReturnsMappedResult()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();
        var command = new GetCartCommand(cart.Id);

        _repository.GetByIdAsync(cart.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult(cart));
        var expectedResult = new GetCartResult
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Status = cart.Status.ToString(),
            CreatedAt = cart.CreatedAt,
            UpdatedAt = cart.UpdatedAt
        };
        _mapper.Map<GetCartResult>(cart).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(cart.Id);
        _repository.Received(1).GetByIdAsync(cart.Id, Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<GetCartResult>(cart);
    }

    /// <summary>
    /// Tests that when the cart is not found, a KeyNotFoundException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given command with non-existing cart When handled Then throws KeyNotFoundException", Skip = "TODO: ajustar depois")]
    public async Task Handle_NonExistingCart_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = new GetCartCommand(Guid.NewGuid());
        _repository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Cart>(null));

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Cart with ID {command.Id} was not found.");
        _repository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
        _logger.Received().Log(
            LogLevel.Warning,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            null,
            Arg.Any<Func<object, Exception?, string>>());
    }

    /// <summary>
    /// Tests that when an exception occurs in the repository, it is logged and rethrown.
    /// </summary>
    [Fact(DisplayName = "When repository throws exception Then handler logs and rethrows", Skip = "TODO: ajustar depois")]
    public async Task Handle_RepositoryThrows_LogsAndThrows()
    {
        // Arrange
        var command = new GetCartCommand(Guid.NewGuid());
        var exception = new Exception("Database failure");
        _repository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Throws(exception);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Database failure");
        _logger.Received().Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }
}
