using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Auth;

/// <summary>
/// Specification that validates if the raw password matches the stored hashed password.
/// </summary>
public class PasswordMustMatchHashedSpecification : ISpecification<(Entities.User User, string InputPassword)>
{
    private readonly IPasswordHasher _passwordHasher;

    public PasswordMustMatchHashedSpecification(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public bool IsSatisfiedBy((Entities.User User, string InputPassword) data)
    {
        return _passwordHasher.VerifyPassword(data.InputPassword, data.User.Password);
    }
}
