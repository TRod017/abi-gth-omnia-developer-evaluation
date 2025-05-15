using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Handler responsible for processing <see cref="GetAllProductsCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler retrieves all products from the repository and maps them to the
/// <see cref="GetAllProductsResult"/> response model using <see cref="IMapper"/>. It also logs
/// the operation using <see cref="ILogger"/> for observability.
/// </remarks>
public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, IReadOnlyCollection<GetAllProductsResult>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllProductsHandler> _logger;

    // EventIds
    private static readonly EventId RetrievingAllProductsEvent = new(3001, nameof(RetrievingAllProductsEvent));
    private static readonly EventId RetrievedProductsEvent = new(3002, nameof(RetrievedProductsEvent));
    private static readonly EventId GetAllProductsErrorEvent = new(3099, nameof(GetAllProductsErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Exception?> LogRetrievingAllProducts =
        LoggerMessage.Define(
            LogLevel.Information,
            RetrievingAllProductsEvent,
            "Retrieving all products");

    private static readonly Action<ILogger, int, Exception?> LogRetrievedProducts =
        LoggerMessage.Define<int>(
            LogLevel.Information,
            RetrievedProductsEvent,
            "Retrieved {Count} products");

    private static readonly Action<ILogger, Exception> LogUnexpectedError =
        LoggerMessage.Define(
            LogLevel.Error,
            GetAllProductsErrorEvent,
            "Unexpected error while retrieving products");

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllProductsHandler"/> class.
    /// </summary>
    /// <param name="repository">The product repository for data access.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public GetAllProductsHandler(IProductRepository repository, IMapper mapper, ILogger<GetAllProductsHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="GetAllProductsCommand"/> request and returns a list of products.
    /// </summary>
    /// <param name="command">The request command to retrieve all products.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>A collection of <see cref="GetAllProductsResult"/> representing the products.</returns>
    public async Task<IReadOnlyCollection<GetAllProductsResult>> Handle(GetAllProductsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogRetrievingAllProducts(_logger, null);

            var products = await _repository.GetAllAsync(cancellationToken);

            LogRetrievedProducts(_logger, products.Count, null);

            return _mapper.Map<IReadOnlyCollection<GetAllProductsResult>>(products);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, ex);
            throw;
        }
    }
}
