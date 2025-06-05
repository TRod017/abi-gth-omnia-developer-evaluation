using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.Specifications.Auth;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

/// <summary>
/// Handles user authentication by verifying credentials and generating a JWT token.
/// Applies domain specifications to ensure the user exists, is active, and the password is valid.
/// </summary>
public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository for accessing user data.</param>
    /// <param name="passwordHasher">Service used to validate password hashes.</param>
    /// <param name="jwtTokenGenerator">Service used to generate JWT tokens.</param>
    public AuthenticateUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    /// <summary>
    /// Handles the user authentication request.
    /// </summary>
    /// <param name="request">The authentication request containing user credentials.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The result containing user information and JWT token if authentication is successful.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if any authentication condition fails.</exception>
    public async Task<AuthenticateUserResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        // Specification: Ensure user exists
        // Specification: Ensure user is active
        // Specification: Ensure password matches
        SpecificationValidator.Validate<Domain.Entities.User>(
            user!,
            (new UserMustExistSpecification(), "User not found"),
            (new UserMustBeActiveSpecification(), "User is not active")
        );

        SpecificationValidator.Validate(
            (user!, request.Password),
            (new PasswordMustMatchHashedSpecification(_passwordHasher), "Invalid credentials")
        );

        var token = _jwtTokenGenerator.GenerateToken(user!);

        return new AuthenticateUserResult
        {
            Token = token,
            Email = user!.Email,
            Name = user.Username,
            Role = user.Role.ToString()
        };
    }
}
