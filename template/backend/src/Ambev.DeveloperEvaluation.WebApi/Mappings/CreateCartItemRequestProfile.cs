using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

/// <summary>
/// AutoMapper profile for mapping CreateCartItemRequest to CreateCartItemCommand.
/// </summary>
public class CreateCartItemRequestProfile : Profile
{
    public CreateCartItemRequestProfile()
    {
        CreateMap<CreateCartItemRequest, CreateCartItemCommand>();
    }
}
