using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DeleteUserHandler> _logger;

    // EventIds
    private static readonly EventId StartDeleteEvent = new(1201, nameof(StartDeleteEvent));
    private static readonly EventId ValidationFailedEvent = new(1202, nameof(ValidationFailedEvent));
    private static readonly EventId NotFoundEvent = new(1203, nameof(NotFoundEvent));
    private static readonly EventId DeletedSuccessEvent = new(1204, nameof(DeletedSuccessEvent));

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="logger">The logger instance</param>
    public DeleteUserHandler(IUserRepository userRepository, ILogger<DeleteUserHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="request">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(StartDeleteEvent, "Handling DeleteUserCommand for ID: {UserId}", request.Id);

        var validator = new DeleteUserValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning(ValidationFailedEvent, "Validation failed for DeleteUserCommand: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var success = await _userRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
        {
            _logger.LogWarning(NotFoundEvent, "User with ID {UserId} not found", request.Id);
            throw new KeyNotFoundException($"User with ID {request.Id} not found");
        }

        _logger.LogInformation(DeletedSuccessEvent, "User with ID {UserId} deleted successfully", request.Id);

        return new DeleteUserResponse { Success = true };
    }
}
