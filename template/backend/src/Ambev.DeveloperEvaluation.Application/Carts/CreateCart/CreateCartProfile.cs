using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// AutoMapper profile for <see cref="CreateCartCommand"/> and <see cref="CreateCartResult"/> mappings.
/// </summary>
/// <remarks>
/// This profile defines the mapping rules between the input command used to create a cart
/// and the domain entity <see cref="Cart"/>, as well as the mapping from the entity to the result model.
/// </remarks>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartProfile"/> class
    /// and defines the mappings for CreateCart feature.
    /// </summary>
    public CreateCartProfile()
    {
        // Mapping from CreateCartCommand to Cart (input → entity)
        CreateMap<CreateCartCommand, Cart>();

        // Mapping from CartItem (entity) to CreateCartItemResult (output DTO)
        CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.CartItem, CreateCartItemResult>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount));

        // Mapping from Cart (entity) to CreateCartResult (output DTO)
        CreateMap<Cart, CreateCartResult>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
