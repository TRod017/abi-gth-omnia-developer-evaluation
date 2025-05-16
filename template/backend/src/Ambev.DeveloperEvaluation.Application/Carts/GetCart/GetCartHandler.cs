using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Handler responsible for processing <see cref="GetCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the incoming cart query using <see cref="GetCartValidator"/>.
/// It then attempts to retrieve the cart by its ID using <see cref="ICartRepository"/>.
/// If found, the cart is mapped to a <see cref="GetCartResult"/> using <see cref="IMapper"/>.
/// Logs are recorded throughout the process using <see cref="ILogger"/> for observability and diagnostics.
/// </remarks>
public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartHandler> _logger;

    // EventIds
    private static readonly EventId FetchingCartEvent = new(3201, nameof(FetchingCartEvent));
    private static readonly EventId ValidationErrorEvent = new(3202, nameof(ValidationErrorEvent));
    private static readonly EventId CartFoundEvent = new(3203, nameof(CartFoundEvent));
    private static readonly EventId CartNotFoundEvent = new(3204, nameof(CartNotFoundEvent));
    private static readonly EventId GetCartErrorEvent = new(3299, nameof(GetCartErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogFetchingCart =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            FetchingCartEvent,
            "Handling GetCartCommand for Cart ID: {CartId}");

    private static readonly Action<ILogger, object, Exception?> LogValidationFailed =
        LoggerMessage.Define<object>(
            LogLevel.Warning,
            ValidationErrorEvent,
            "Validation failed for GetCartCommand. Errors: {@Errors}");

    private static readonly Action<ILogger, Guid, Exception?> LogCartFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            CartFoundEvent,
            "Cart with ID {CartId} retrieved successfully");

    private static readonly Action<ILogger, Guid, Exception?> LogCartNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            CartNotFoundEvent,
            "Cart with ID {CartId} not found");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            GetCartErrorEvent,
            "Unexpected error occurred while handling GetCartCommand for ID: {CartId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public GetCartHandler(ICartRepository repository, IMapper mapper, ILogger<GetCartHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="GetCartCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the cart ID to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The retrieved cart details as <see cref="GetCartResult"/>.</returns>
    public async Task<GetCartResult> Handle(GetCartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogFetchingCart(_logger, command.Id, null);

            var validator = new GetCartValidator();
            var validation = await validator.ValidateAsync(command, cancellationToken);

            if (!validation.IsValid)
            {
                LogValidationFailed(_logger, validation.Errors, null);
                throw new ValidationException(validation.Errors);
            }

            var cart = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (cart == null)
            {
                LogCartNotFound(_logger, command.Id, null);
                throw new KeyNotFoundException($"Cart with ID {command.Id} was not found.");
            }

            LogCartFound(_logger, command.Id, null);

            return _mapper.Map<GetCartResult>(cart);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
