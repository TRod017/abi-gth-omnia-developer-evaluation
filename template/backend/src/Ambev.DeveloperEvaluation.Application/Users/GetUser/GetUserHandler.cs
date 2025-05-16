using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Handler for processing GetUserCommand requests
/// </summary>
public class GetUserHandler : IRequestHandler<GetUserCommand, GetUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserHandler> _logger;

    // EventIds
    private static readonly EventId StartGetEvent = new(1101, nameof(StartGetEvent));
    private static readonly EventId ValidationFailedEvent = new(1102, nameof(ValidationFailedEvent));
    private static readonly EventId NotFoundEvent = new(1103, nameof(NotFoundEvent));
    private static readonly EventId SuccessEvent = new(1104, nameof(SuccessEvent));

    /// <summary>
    /// Initializes a new instance of GetUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="logger">The logger instance</param>
    public GetUserHandler(
        IUserRepository userRepository,
        IMapper mapper,
        ILogger<GetUserHandler> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the GetUserCommand request
    /// </summary>
    /// <param name="request">The GetUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    public async Task<GetUserResult> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(StartGetEvent, "Handling GetUserCommand for ID: {UserId}", request.Id);

        var validator = new GetUserValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning(ValidationFailedEvent, "Validation failed for GetUserCommand: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning(NotFoundEvent, "User with ID {UserId} not found", request.Id);
            throw new KeyNotFoundException($"User with ID {request.Id} not found");
        }

        _logger.LogInformation(SuccessEvent, "User with ID {UserId} retrieved successfully", request.Id);

        return _mapper.Map<GetUserResult>(user);
    }
}
