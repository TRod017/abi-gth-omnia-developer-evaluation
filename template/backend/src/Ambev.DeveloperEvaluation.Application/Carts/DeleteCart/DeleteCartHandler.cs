using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Handler responsible for processing <see cref="DeleteCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler attempts to delete the cart from the repository using <see cref="ICartRepository"/>.
/// and logs the operation using <see cref="ILogger"/>. Returns <c>true</c> if the deletion is successful,
/// or <c>false</c> if the cart was not found.
/// Validation is handled by MediatR pipeline behavior.
/// </remarks>
public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, bool>
{
    private readonly ICartRepository _repository;
    private readonly ILogger<DeleteCartHandler> _logger;

    // EventIds
    private static readonly EventId DeletingCartEvent = new(1101, nameof(DeletingCartEvent));
    private static readonly EventId CartDeletedEvent = new(1102, nameof(CartDeletedEvent));
    private static readonly EventId CartNotFoundEvent = new(1103, nameof(CartNotFoundEvent));
    private static readonly EventId DeleteCartErrorEvent = new(1199, nameof(DeleteCartErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogDeletingCart =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            DeletingCartEvent,
            "Handling DeleteCartCommand for ID: {CartId}");

    private static readonly Action<ILogger, Guid, Exception?> LogCartDeleted =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            CartDeletedEvent,
            "Cart with ID {CartId} deleted successfully");

    private static readonly Action<ILogger, Guid, Exception?> LogCartNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            CartNotFoundEvent,
            "Cart with ID {CartId} not found");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            DeleteCartErrorEvent,
            "Unexpected error while deleting cart with ID {CartId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository instance.</param>
    /// <param name="logger">The logger instance.</param>
    public DeleteCartHandler(ICartRepository repository, ILogger<DeleteCartHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="DeleteCartCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the cart ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the cart was successfully deleted; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogDeletingCart(_logger, command.Id, null);

            var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);

            if (deleted)
                LogCartDeleted(_logger, command.Id, null);
            else
                LogCartNotFound(_logger, command.Id, null);

            return deleted;
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
