using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItems;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// AutoMapper profile for mapping between Cart entity and GetCartResult model.
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCart operation.
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<Cart, GetCartResult>();
        CreateMap<CartItem, GetCartItemResult>();
    }
}
