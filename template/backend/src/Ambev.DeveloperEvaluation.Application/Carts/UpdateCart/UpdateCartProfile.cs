using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// AutoMapper profile for updating a cart.
/// </summary>
public class UpdateCartProfile : Profile
{
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartCommand, Cart>();
        CreateMap<UpdateCartItemCommand, CartItem>();
        CreateMap<Cart, UpdateCartResult>();
    }
}
