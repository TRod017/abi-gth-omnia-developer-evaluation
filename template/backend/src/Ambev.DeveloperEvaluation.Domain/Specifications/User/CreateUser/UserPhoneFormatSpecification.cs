using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.User.CreateUser;

/// <summary>
/// Specification that validates if the phone number follows Brazilian format.
/// </summary>
public class UserPhoneFormatSpecification : ISpecification<Entities.User>
{
    public bool IsSatisfiedBy(Entities.User user)
    {
        return Regex.IsMatch(user.Phone, @"^\+55\d{10,11}$");
    }
}
