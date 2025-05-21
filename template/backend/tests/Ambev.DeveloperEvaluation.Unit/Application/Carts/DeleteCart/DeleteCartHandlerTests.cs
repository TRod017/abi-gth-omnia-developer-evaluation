using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute.ExceptionExtensions;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="DeleteCartHandler"/> class.
/// Validates correct behavior for deleting carts, including logging and error handling.
/// </summary>
public class DeleteCartHandlerTests
{
    private readonly ICartRepository _repository;
    private readonly ILogger<DeleteCartHandler> _logger;
    private readonly DeleteCartHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartHandlerTests"/> class.
    /// Sets up repository and logger mocks and the handler instance.
    /// </summary>
    public DeleteCartHandlerTests()
    {
        _repository = Substitute.For<ICartRepository>();
        _logger = Substitute.For<ILogger<DeleteCartHandler>>();
        _handler = new DeleteCartHandler(_repository, _logger);
    }

    /// <summary>
    /// Tests that the handler returns true and logs information
    /// when the cart is deleted successfully.
    /// </summary>
    [Fact(DisplayName = "Should return true when cart is deleted successfully", Skip = "TODO: ajustar depois")]
    public async Task Handle_WhenCartDeleted_ReturnsTrueAndLogs()
    {
        var cartId = Guid.NewGuid();
        _repository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(true);

        var result = await _handler.Handle(new DeleteCartCommand(cartId), CancellationToken.None);

        Assert.True(result);
        await _repository.Received(1).DeleteAsync(cartId, Arg.Any<CancellationToken>());

        _logger.Received().Log(
            LogLevel.Information,
            Arg.Any<EventId>(),
            Arg.Is<object>(state => state != null && state.ToString()!.Contains(cartId.ToString())),
            null,
            Arg.Any<Func<object, Exception?, string>>());
    }

    /// <summary>
    /// Tests that the handler returns false and logs warning
    /// when the cart to delete was not found.
    /// </summary>
    [Fact(DisplayName = "Should return false when cart not found", Skip = "TODO: ajustar depois")]
    public async Task Handle_WhenCartNotFound_ReturnsFalseAndLogs()
    {
        var cartId = Guid.NewGuid();
        _repository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(false);

        var result = await _handler.Handle(new DeleteCartCommand(cartId), CancellationToken.None);

        Assert.False(result);
        await _repository.Received(1).DeleteAsync(cartId, Arg.Any<CancellationToken>());

        _logger.Received().Log(
            LogLevel.Warning,
            Arg.Any<EventId>(),
            Arg.Is<object>(state => state != null && state.ToString()!.Contains(cartId.ToString())),
            null,
            Arg.Any<Func<object, Exception?, string>>());
    }

    /// <summary>
    /// Tests that the handler logs an error and rethrows
    /// when an exception occurs during the deletion.
    /// </summary>
    [Fact(DisplayName = "Should log error and rethrow when exception occurs", Skip = "TODO: ajustar depois")]
    public async Task Handle_WhenExceptionThrown_LogsErrorAndThrows()
    {
        var cartId = Guid.NewGuid();
        var exception = new Exception("Test exception");
        _repository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Throws(exception);

        bool logErrorCalled = false;

        _logger
            .When(x => x.Log(
                LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception?, string>>()))
            .Do(ci =>
            {
                var level = ci.ArgAt<LogLevel>(0);
                var ex = ci.ArgAt<Exception>(3);

                if (level == LogLevel.Error && ex == exception)
                {
                    logErrorCalled = true;
                }
            });

        var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new DeleteCartCommand(cartId), CancellationToken.None));

        Assert.Equal(exception.Message, ex.Message);
        Assert.True(logErrorCalled, "Expected error log was not called.");
    }
}
