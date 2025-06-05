using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.User.CreateUser;

/// <summary>
/// Specification that validates if the password meets minimum strength requirements.
/// </summary>
public class StrongPasswordSpecification : ISpecification<Entities.User>
{
    public bool IsSatisfiedBy(Entities.User user)
    {
        return Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
    }
}
