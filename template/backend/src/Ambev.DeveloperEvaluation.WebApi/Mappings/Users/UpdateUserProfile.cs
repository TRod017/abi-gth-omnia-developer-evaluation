using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Users;

/// <summary>
/// AutoMapper profile for mapping between Web API models and application layer models
/// in the UpdateUser use case.
/// </summary>
/// <remarks>
/// Defines the mappings used to:
/// - Convert <see cref="UpdateUserRequest"/> into <see cref="UpdateUserCommand"/>
/// - Convert <see cref="UpdateUserResult"/> into <see cref="UpdateUserResponse"/>
/// </remarks>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserProfile"/> class
    /// and configures the mappings for the UpdateUser operation.
    /// </summary>
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserRequest, UpdateUserCommand>();
        CreateMap<UpdateUserResult, UpdateUserResponse>();
    }
}
