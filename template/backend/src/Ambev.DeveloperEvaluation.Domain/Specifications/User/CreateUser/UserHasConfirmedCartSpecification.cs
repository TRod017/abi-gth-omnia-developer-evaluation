using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.User.CreateUser;

/// <summary>
/// Specification that ensures the user has at least one confirmed cart.
/// </summary>
public class UserHasConfirmedCartSpecification : ISpecification<Entities.User>
{
    public bool IsSatisfiedBy(Entities.User user)
    {
        return user.Carts?.Any(c => c.Status == CartStatus.Confirmed) ?? false;
    }
}

