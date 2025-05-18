using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;

/// <summary>
/// AutoMapper profile for mapping between <see cref="GetAllUsersResult"/> (application layer)
/// and <see cref="GetAllUsersResponse"/> (API layer).
/// </summary>
/// <remarks>
/// Defines the mapping used to translate application output models
/// into API response models for the GetAllUsers endpoint.
/// </remarks>
public class GetAllUsersProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllUsersProfile"/> class
    /// and sets up the mapping from result to response.
    /// </summary>
    public GetAllUsersProfile()
    {
        // Application → WebApi
        CreateMap<GetAllUsersResult, GetAllUsersResponse>();

        // WebApi → Application
        CreateMap<GetAllUsersRequest, GetAllUsersCommand>();
    }
}
