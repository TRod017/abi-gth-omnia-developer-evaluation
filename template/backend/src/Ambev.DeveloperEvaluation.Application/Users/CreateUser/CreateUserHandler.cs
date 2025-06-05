using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Specifications.User.CreateUser; // Specification: importações

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<CreateUserHandler> _logger;

    // EventIds
    private static readonly EventId StartEvent = new(1001, nameof(StartEvent));
    private static readonly EventId UserExistsEvent = new(1003, nameof(UserExistsEvent));
    private static readonly EventId SuccessEvent = new(1004, nameof(SuccessEvent));

    /// <summary>
    /// Initializes a new instance of CreateUserHandler
    /// </summary>
    public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, ILogger<CreateUserHandler> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CreateUserCommand request
    /// </summary>
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(StartEvent, "Handling CreateUserCommand for email: {Email}", command.Email);

        var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        var user = _mapper.Map<User>(command);

        // Specification: Ensure email is unique
        var uniqueEmailSpec = new UniqueEmailSpecification(_userRepository);
        if (!await uniqueEmailSpec.IsSatisfiedByAsync(user, cancellationToken))
        {
            _logger.LogWarning(UserExistsEvent, "User with email {Email} already exists", command.Email);
            throw new InvalidOperationException($"User with email {command.Email} already exists");
        }

        // Specification: Ensure email is valid
        var validEmailSpec = new ValidEmailSpecification();
        if (!validEmailSpec.IsSatisfiedBy(user))
            throw new InvalidOperationException("Invalid email format");

        // Specification: Ensure password is strong
        var strongPasswordSpec = new StrongPasswordSpecification();
        if (!strongPasswordSpec.IsSatisfiedBy(user))
            throw new InvalidOperationException("Password does not meet security requirements");

        // Specification: Ensure phone format is valid
        var phoneFormatSpec = new UserPhoneFormatSpecification();
        if (!phoneFormatSpec.IsSatisfiedBy(user))
            throw new InvalidOperationException("Phone number format is invalid");

        user.EnsureBusinessRulesAreMet();

        user.Password = _passwordHasher.HashPassword(command.Password);

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
        var result = _mapper.Map<CreateUserResult>(createdUser);

        _logger.LogInformation(SuccessEvent, "User created successfully with ID: {Id}", createdUser.Id);

        return result;
    }
}
