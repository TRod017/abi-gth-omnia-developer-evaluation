using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItems;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Profile for mapping between GetCart Application and API.
/// </summary>
public class GetCartProfile : Profile
{
    public GetCartProfile()
    {
        CreateMap<GetCartResult, GetCartResponse>();
        CreateMap<GetCartItemResult, GetCartItemResponse>();
    }
}
