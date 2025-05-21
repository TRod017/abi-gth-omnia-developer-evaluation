using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// AutoMapper profile for mapping between <see cref="User"/> entity and <see cref="GetAllUsersResult"/>.
/// </summary>
/// <remarks>
/// Defines the mapping used to project user domain entities into response models returned by the
/// GetAllUsers use case.
/// </remarks>
public class GetAllUsersProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllUsersProfile"/> class
    /// and configures the mapping from <see cref="User"/> to <see cref="GetAllUsersResult"/>.
    /// </summary>
    public GetAllUsersProfile()
    {
        CreateMap<User, GetAllUsersResult>();
    }
}
