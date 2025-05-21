using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// AutoMapper profile for mapping between <see cref="Cart"/> entity and <see cref="GetAllCartsResult"/>.
/// </summary>
/// <remarks>
/// Defines the mapping used to project cart domain entities into response models returned by the
/// GetAllCarts use case.
/// </remarks>
public class GetAllCartsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCartsProfile"/> class
    /// and configures the mapping from <see cref="Cart"/> to <see cref="GetAllCartsResult"/>.
    /// </summary>
    public GetAllCartsProfile()
    {
        CreateMap < Ambev.DeveloperEvaluation.Domain.Entities.CartItem, CreateCartItemResult>();

        CreateMap<Cart, GetAllCartsResult>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
