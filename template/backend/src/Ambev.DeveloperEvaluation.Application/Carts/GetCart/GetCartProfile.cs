using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItem;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// AutoMapper profile for mapping between <see cref="Cart"/> and <see cref="GetCartResult"/>,
/// as well as between <see cref="CartItem"/> and <see cref="GetCartItemResult"/>.
/// </summary>
/// <remarks>
/// Defines the object-object mapping used to convert cart domain entities
/// into response models used in the GetCart use case.
/// </remarks>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartProfile"/> class
    /// and sets up the mappings for GetCart operation.
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<Cart, GetCartResult>();
        CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.CartItem, GetCartItemResult>();
    }
}
