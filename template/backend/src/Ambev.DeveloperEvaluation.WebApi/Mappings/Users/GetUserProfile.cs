using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Users;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        // Mapeamento de Guid --> GetUserCommand
        CreateMap<Guid, Application.Users.GetUser.GetUserCommand>()
            .ConstructUsing(id => new Application.Users.GetUser.GetUserCommand(id));

        // Mapeamento de GetUserResult --> GetUserResponse (nomes idênticos)
        CreateMap<Application.Users.GetUser.GetUserResult, GetUserResponse>();

        // Mapeamento manual entre User --> GetUserResult
        CreateMap<Domain.Entities.User, Application.Users.GetUser.GetUserResult>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));
    }
}
