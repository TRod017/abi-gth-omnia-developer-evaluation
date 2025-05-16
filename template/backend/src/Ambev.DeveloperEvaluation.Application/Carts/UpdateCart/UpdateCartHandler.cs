using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Handler responsible for processing <see cref="UpdateCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the update command using <see cref="UpdateCartValidator"/>.
/// It retrieves the existing cart from the repository, applies updates using <see cref="IMapper"/>.
/// persists the changes, and logs each step using <see cref="ILogger"/>. 
/// Returns <see cref="UpdateCartResult"/> upon successful update or throws exceptions in case of errors.
/// </remarks>
public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartHandler> _logger;

    // EventIds
    private static readonly EventId StartUpdateEvent = new(3301, nameof(StartUpdateEvent));
    private static readonly EventId ValidationFailedEvent = new(3302, nameof(ValidationFailedEvent));
    private static readonly EventId CartNotFoundEvent = new(3303, nameof(CartNotFoundEvent));
    private static readonly EventId CartUpdatedEvent = new(3304, nameof(CartUpdatedEvent));
    private static readonly EventId UnexpectedErrorEvent = new(3399, nameof(UnexpectedErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogStartUpdate =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            StartUpdateEvent,
            "Handling UpdateCartCommand for ID: {CartId}");

    private static readonly Action<ILogger, object, Exception?> LogValidationFailed =
        LoggerMessage.Define<object>(
            LogLevel.Warning,
            ValidationFailedEvent,
            "Validation failed for UpdateCartCommand. Errors: {@Errors}");

    private static readonly Action<ILogger, Guid, Exception?> LogCartNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            CartNotFoundEvent,
            "Cart with ID {CartId} not found");

    private static readonly Action<ILogger, Guid, Exception?> LogCartUpdated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            CartUpdatedEvent,
            "Cart with ID {CartId} updated successfully");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            UnexpectedErrorEvent,
            "Unexpected error while updating cart with ID {CartId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public UpdateCartHandler(ICartRepository repository, IMapper mapper, ILogger<UpdateCartHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="UpdateCartCommand"/> request and returns the updated cart.
    /// </summary>
    /// <param name="command">The command containing updated cart information.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The updated cart result as <see cref="UpdateCartResult"/>.</returns>
    public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogStartUpdate(_logger, command.Id, null);

            var validator = new UpdateCartValidator();
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

            _mapper.Map(command, cart);
            var updated = await _repository.UpdateAsync(cart, cancellationToken);

            LogCartUpdated(_logger, updated.Id, null);

            return _mapper.Map<UpdateCartResult>(updated);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
