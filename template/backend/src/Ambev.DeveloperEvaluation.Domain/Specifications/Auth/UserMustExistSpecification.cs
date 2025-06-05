namespace Ambev.DeveloperEvaluation.Domain.Specifications.Auth;

/// <summary>
/// Specification that validates if the user exists in the system.
/// </summary>
public class UserMustExistSpecification : ISpecification<Entities.User>
{
    public bool IsSatisfiedBy(Entities.User user)
    {
        return user != null;
    }
}
