using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handler responsible for processing <see cref="DeleteSaleCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler attempts to delete the Sale from the repository using <see cref="ISaleRepository"/>.
/// and logs the operation using <see cref="ILogger"/>. Returns <c>true</c> if the deletion is successful,
/// or <c>false</c> if the Sale was not found.
/// Validation is handled by MediatR pipeline behavior.
/// </remarks>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, bool>
{
    private readonly ISaleRepository _repository;
    private readonly ILogger<DeleteSaleHandler> _logger;

    // EventIds
    private static readonly EventId DeletingSaleEvent = new(1101, nameof(DeletingSaleEvent));
    private static readonly EventId SaleDeletedEvent = new(1102, nameof(SaleDeletedEvent));
    private static readonly EventId SaleNotFoundEvent = new(1103, nameof(SaleNotFoundEvent));
    private static readonly EventId DeleteSaleErrorEvent = new(1199, nameof(DeleteSaleErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogDeletingSale =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            DeletingSaleEvent,
            "Handling DeleteSaleCommand for ID: {SaleId}");

    private static readonly Action<ILogger, Guid, Exception?> LogSaleDeleted =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            SaleDeletedEvent,
            "Sale with ID {SaleId} deleted successfully");

    private static readonly Action<ILogger, Guid, Exception?> LogSaleNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            SaleNotFoundEvent,
            "Sale with ID {SaleId} not found");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            DeleteSaleErrorEvent,
            "Unexpected error while deleting Sale with ID {SaleId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandler"/> class.
    /// </summary>
    /// <param name="repository">The Sale repository instance.</param>
    /// <param name="logger">The logger instance.</param>
    public DeleteSaleHandler(ISaleRepository repository, ILogger<DeleteSaleHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="DeleteSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the Sale ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the Sale was successfully deleted; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogDeletingSale(_logger, command.Id, null);

            var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);

            if (deleted)
                LogSaleDeleted(_logger, command.Id, null);
            else
                LogSaleNotFound(_logger, command.Id, null);

            return deleted;
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
