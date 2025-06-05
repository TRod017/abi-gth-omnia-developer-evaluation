using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Auth;

/// <summary>
/// Specification that validates if the user status is Active.
/// </summary>
public class UserMustBeActiveSpecification : ISpecification<Entities.User>
{
    public bool IsSatisfiedBy(Entities.User user)
    {
        return user.Status == UserStatus.Active;
    }
}
