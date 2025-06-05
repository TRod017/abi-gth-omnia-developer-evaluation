using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.User.CreateUser;

/// <summary>
/// Specification that validates if the email format is valid.
/// </summary>
public class ValidEmailSpecification : ISpecification<Entities.User>
{
    public bool IsSatisfiedBy(Entities.User user)
    {
        return Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
