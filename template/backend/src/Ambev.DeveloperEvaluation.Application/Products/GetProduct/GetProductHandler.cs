using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Handler for processing <see cref="GetProductCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler retrieves a product by its ID using <see cref="IProductRepository"/>.
/// If the product is found, it is mapped to <see cref="GetProductResult"/> and returned.
/// If not found, a null response is returned. Logging is used throughout the process
/// via <see cref="ILogger"/>.
/// </remarks>
public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult?>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductHandler> _logger;

    // EventIds
    private static readonly EventId FetchingProductEvent = new(3101, nameof(FetchingProductEvent));
    private static readonly EventId ProductFoundEvent = new(3102, nameof(ProductFoundEvent));
    private static readonly EventId ProductNotFoundEvent = new(3103, nameof(ProductNotFoundEvent));
    private static readonly EventId GetProductErrorEvent = new(3199, nameof(GetProductErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogFetchingProduct =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            FetchingProductEvent,
            "Fetching product with ID: {ProductId}");

    private static readonly Action<ILogger, Guid, Exception?> LogProductFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            ProductFoundEvent,
            "Product with ID {ProductId} found");

    private static readonly Action<ILogger, Guid, Exception?> LogProductNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            ProductNotFoundEvent,
            "Product with ID {ProductId} not found");

    private static readonly Action<ILogger, Exception> LogUnexpectedError =
        LoggerMessage.Define(
            LogLevel.Error,
            GetProductErrorEvent,
            "Unexpected error while retrieving product");

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductHandler"/> class.
    /// </summary>
    /// <param name="repository">The product repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public GetProductHandler(IProductRepository repository, IMapper mapper, ILogger<GetProductHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="GetProductCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the product ID to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The retrieved product details, or null if not found.</returns>
    public async Task<GetProductResult?> Handle(GetProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogFetchingProduct(_logger, command.Id, null);

            var product = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (product == null)
            {
                LogProductNotFound(_logger, command.Id, null);
                return null;
            }

            LogProductFound(_logger, command.Id, null);

            return _mapper.Map<GetProductResult>(product);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, ex);
            throw;
        }
    }
}
