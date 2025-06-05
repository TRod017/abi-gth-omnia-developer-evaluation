using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.User.CreateUser;

/// <summary>
/// Specification that validates if the email is unique in the user repository.
/// </summary>
public class UniqueEmailSpecification : IAsyncSpecification<Entities.User>
{
    private readonly IUserRepository _userRepository;

    public UniqueEmailSpecification(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsSatisfiedByAsync(Entities.User user, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userRepository.GetByEmailAsync(user.Email, cancellationToken);
        return existingUser == null || existingUser.Id == user.Id;
    }
}
