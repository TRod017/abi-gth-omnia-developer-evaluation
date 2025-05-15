using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handler for processing <see cref="DeleteProductCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler deletes a product by its unique identifier. It logs the operation and returns
/// a boolean indicating whether the product was successfully deleted.
/// </remarks>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<DeleteProductHandler> _logger;

    // EventIds
    private static readonly EventId DeletingProductEvent = new(2001, nameof(DeletingProductEvent));
    private static readonly EventId ProductDeletedEvent = new(2002, nameof(ProductDeletedEvent));
    private static readonly EventId ProductNotFoundEvent = new(2003, nameof(ProductNotFoundEvent));
    private static readonly EventId DeleteProductErrorEvent = new(2099, nameof(DeleteProductErrorEvent));

    // LoggerMessage definitions (high performance)
    private static readonly Action<ILogger, Guid, Exception?> LogDeletingProduct =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            DeletingProductEvent,
            "Deleting product with ID: {ProductId}");

    private static readonly Action<ILogger, Guid, Exception?> LogProductDeleted =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            ProductDeletedEvent,
            "Product with ID {ProductId} deleted successfully");

    private static readonly Action<ILogger, Guid, Exception?> LogProductNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            ProductNotFoundEvent,
            "Product with ID {ProductId} not found");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            DeleteProductErrorEvent,
            "Unexpected error while deleting product with ID: {ProductId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductHandler"/> class.
    /// </summary>
    /// <param name="repository">The product repository instance.</param>
    /// <param name="logger">The logger instance.</param>
    public DeleteProductHandler(IProductRepository repository, ILogger<DeleteProductHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="DeleteProductCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the product ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the product was successfully deleted; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogDeletingProduct(_logger, command.Id, null);

            var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);

            if (deleted)
                LogProductDeleted(_logger, command.Id, null);
            else
                LogProductNotFound(_logger, command.Id, null);

            return deleted;
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
