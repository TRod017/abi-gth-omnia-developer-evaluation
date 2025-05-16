using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// AutoMapper profile for mapping between <see cref="UpdateUserCommand"/>, <see cref="User"/> entity,
/// and <see cref="UpdateUserResult"/>.
/// </summary>
/// <remarks>
/// Defines the mappings required for the UpdateUser use case, allowing the system to:
/// - Convert a command into a user entity to persist updates.
/// - Map the updated entity back into a result object to return from the handler.
/// </remarks>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserProfile"/> class
    /// and configures the mappings for the UpdateUser operation.
    /// </summary>
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserCommand, User>();
        CreateMap<User, UpdateUserResult>();
    }
}
