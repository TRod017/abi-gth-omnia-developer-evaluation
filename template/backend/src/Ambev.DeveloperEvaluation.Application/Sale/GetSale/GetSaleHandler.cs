using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler responsible for processing <see cref="GetSaleCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler relies on MediatR's pipeline behavior to automatically perform validation using
/// registered FluentValidation validators, so explicit validation calls in this handler are removed.
/// It then attempts to retrieve the Sale by its ID using <see cref="ISaleRepository"/>.
/// If found, the Sale is mapped to a <see cref="GetSaleResult"/> using <see cref="IMapper"/>.
/// Logs are recorded throughout the process using <see cref="ILogger"/> for observability and diagnostics.
/// </remarks>
public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleHandler> _logger;

    // EventIds
    private static readonly EventId FetchingSaleEvent = new(3201, nameof(FetchingSaleEvent));
    private static readonly EventId SaleFoundEvent = new(3203, nameof(SaleFoundEvent));
    private static readonly EventId SaleNotFoundEvent = new(3204, nameof(SaleNotFoundEvent));
    private static readonly EventId GetSaleErrorEvent = new(3299, nameof(GetSaleErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogFetchingSale =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            FetchingSaleEvent,
            "Handling GetSaleCommand for Sale ID: {SaleId}");

    private static readonly Action<ILogger, Guid, Exception?> LogSaleFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            SaleFoundEvent,
            "Sale with ID {SaleId} retrieved successfully");

    private static readonly Action<ILogger, Guid, Exception?> LogSaleNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            SaleNotFoundEvent,
            "Sale with ID {SaleId} not found");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            GetSaleErrorEvent,
            "Unexpected error occurred while handling GetSaleCommand for ID: {SaleId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
    /// </summary>
    /// <param name="repository">The Sale repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public GetSaleHandler(ISaleRepository repository, IMapper mapper, ILogger<GetSaleHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="GetSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the Sale ID to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The retrieved Sale details as <see cref="GetSaleResult"/>.</returns>
    public async Task<GetSaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogFetchingSale(_logger, command.Id, null);

            var Sale = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (Sale == null)
            {
                LogSaleNotFound(_logger, command.Id, null);
                throw new KeyNotFoundException($"Sale with ID {command.Id} was not found.");
            }

            LogSaleFound(_logger, command.Id, null);

            return _mapper.Map<GetSaleResult>(Sale);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
