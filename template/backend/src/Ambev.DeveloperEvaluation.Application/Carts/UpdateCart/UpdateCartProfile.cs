using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// AutoMapper profile for mapping between <see cref="UpdateCartCommand"/>, <see cref="Cart"/> entity,
/// <see cref="UpdateCartItemCommand"/>, <see cref="CartItem"/>, and <see cref="UpdateCartResult"/>.
/// </summary>
/// <remarks>
/// Defines the mappings required for the UpdateCart use case, allowing the system to:
/// - Convert update commands into domain entities for persistence.
/// - Map updated entities back into result objects returned by the handler.
/// </remarks>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartProfile"/> class
    /// and configures the mappings for the UpdateCart operation.
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartCommand, Cart>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateCartItemCommand, CartItem>()
            .ForMember(dest => dest.CartId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductName, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Cart, UpdateCartResult>();
    }
}
