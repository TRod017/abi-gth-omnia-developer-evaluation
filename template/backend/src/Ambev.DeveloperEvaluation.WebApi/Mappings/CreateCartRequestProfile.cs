using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

/// <summary>
/// AutoMapper profile for mapping CreateCartRequest to CreateCartCommand.
/// </summary>
public class CreateCartRequestProfile : Profile
{
    public CreateCartRequestProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>();
    }
}
