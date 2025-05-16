using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// Query to retrieve all users.
/// </summary>
/// <remarks>
/// This command is used to retrieve a complete list of users in the system. 
/// It returns a collection of <see cref="GetAllUsersResult"/> upon execution.
/// </remarks>
public class GetAllUsersCommand : IRequest<IReadOnlyCollection<GetAllUsersResult>>
{
}
