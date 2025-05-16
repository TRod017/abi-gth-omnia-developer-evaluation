using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Handler responsible for processing <see cref="UpdateUserCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the update command using <see cref="UpdateUserValidator"/>.
/// It retrieves the existing user from the repository, applies updates using <see cref="IMapper"/>.
/// Persists the changes, and logs each step using <see cref="ILogger"/>. 
/// Returns <see cref="UpdateUserResult"/> upon successful update or throws exceptions in case of errors.
/// </remarks>
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult?>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateUserHandler> _logger;

    // EventIds
    private static readonly EventId StartUpdateEvent = new(3401, nameof(StartUpdateEvent));
    private static readonly EventId ValidationFailedEvent = new(3402, nameof(ValidationFailedEvent));
    private static readonly EventId UserNotFoundEvent = new(3403, nameof(UserNotFoundEvent));
    private static readonly EventId UserUpdatedEvent = new(3404, nameof(UserUpdatedEvent));
    private static readonly EventId UnexpectedErrorEvent = new(3499, nameof(UnexpectedErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogStartUpdate =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            StartUpdateEvent,
            "Handling UpdateUserCommand for ID: {UserId}");

    private static readonly Action<ILogger, object, Exception?> LogValidationFailed =
        LoggerMessage.Define<object>(
            LogLevel.Warning,
            ValidationFailedEvent,
            "Validation failed for UpdateUserCommand. Errors: {@Errors}");

    private static readonly Action<ILogger, Guid, Exception?> LogUserNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            UserNotFoundEvent,
            "User with ID {UserId} not found");

    private static readonly Action<ILogger, Guid, Exception?> LogUserUpdated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            UserUpdatedEvent,
            "User with ID {UserId} updated successfully");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            UnexpectedErrorEvent,
            "Unexpected error while updating user with ID {UserId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserHandler"/> class.
    /// </summary>
    /// <param name="repository">The user repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public UpdateUserHandler(IUserRepository repository, IMapper mapper, ILogger<UpdateUserHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="UpdateUserCommand"/> request and returns the updated user.
    /// </summary>
    /// <param name="command">The command containing updated user information.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The updated user result as <see cref="UpdateUserResult"/>.</returns>
    public async Task<UpdateUserResult?> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogStartUpdate(_logger, command.Id, null);

            var validator = new UpdateUserValidator();
            var validation = await validator.ValidateAsync(command, cancellationToken);

            if (!validation.IsValid)
            {
                LogValidationFailed(_logger, validation.Errors, null);
                throw new ValidationException(validation.Errors);
            }

            var user = await _repository.GetByIdAsync(command.Id, cancellationToken);
            
            if (user == null)
            {
                LogUserNotFound(_logger, command.Id, null);
                return null;
            }

            _mapper.Map(command, user);
            var updated = await _repository.UpdateAsync(user, cancellationToken);

            LogUserUpdated(_logger, updated.Id, null);

            return _mapper.Map<UpdateUserResult>(updated);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
