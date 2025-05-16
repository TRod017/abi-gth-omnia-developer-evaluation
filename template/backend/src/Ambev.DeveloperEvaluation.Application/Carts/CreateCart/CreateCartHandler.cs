using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Handler responsible for processing <see cref="CreateCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the incoming cart creation command using <see cref="CreateCartValidator"/>.
/// It maps the command to a <see cref="Cart"/> entity using <see cref="IMapper"/>,
/// persists it using <see cref="ICartRepository"/>, and logs each step of the operation
/// using <see cref="ILogger"/>. Returns a <see cref="CreateCartResult"/> upon successful creation.
/// </remarks>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;

    private static readonly EventId HandlingEvent = new(1001, nameof(CreateCartHandler));
    private static readonly EventId MappingEvent = new(1002, "Mapping");
    private static readonly EventId PersistenceEvent = new(1003, "Persistence");
    private static readonly EventId ValidationErrorEvent = new(1400, "ValidationError");
    private static readonly EventId UnhandledExceptionEvent = new(1500, "UnhandledException");

    private static readonly Action<ILogger, Guid, Exception?> LogHandling =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            HandlingEvent,
            "Handling CreateCartCommand for UserId: {UserId}");

    private static readonly Action<ILogger, object, Exception?> LogValidationFailed =
        LoggerMessage.Define<object>(
            LogLevel.Warning,
            ValidationErrorEvent,
            "Validation failed for CreateCartCommand. Errors: {@Errors}");

    private static readonly Action<ILogger, object, Exception?> LogMapped =
        LoggerMessage.Define<object>(
            LogLevel.Debug,
            MappingEvent,
            "Mapped CreateCartCommand to Cart entity: {@Cart}");

    private static readonly Action<ILogger, Guid, Exception?> LogCreated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            PersistenceEvent,
            "Cart created with ID: {CartId}");

    private static readonly Action<ILogger, Guid, Exception> LogUnhandled =
        LoggerMessage.Define<Guid>(
            LogLevel.Critical,
            UnhandledExceptionEvent,
            "Unhandled exception occurred while creating cart for UserId: {UserId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository for persistence operations.</param>
    /// <param name="mapper">The AutoMapper instance used for object mapping.</param>
    /// <param name="logger">The logger used to log operational steps.</param>
    public CreateCartHandler(ICartRepository repository, IMapper mapper, ILogger<CreateCartHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CreateCartCommand"/> request and returns the created cart result.
    /// </summary>
    /// <param name="command">The command containing the cart data to be created.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The result of the created cart as <see cref="CreateCartResult"/>.</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        LogHandling(_logger, command.UserId, null);

        var validator = new CreateCartValidator();
        var validation = await validator.ValidateAsync(command, cancellationToken);

        if (!validation.IsValid)
        {
            LogValidationFailed(_logger, validation.Errors, null);
            throw new ValidationException(validation.Errors);
        }

        try
        {
            var cart = _mapper.Map<Cart>(command);

            LogMapped(_logger, cart, null);

            var created = await _repository.CreateAsync(cart, cancellationToken);

            LogCreated(_logger, created.Id, null);

            return _mapper.Map<CreateCartResult>(created);
        }
        catch (Exception ex)
        {
            LogUnhandled(_logger, command.UserId, ex);
            throw;
        }
    }
}
