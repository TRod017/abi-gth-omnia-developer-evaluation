using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts.CartItem;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts.CartItem;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Carts;

/// <summary>
/// AutoMapper profile for mapping between application layer models and Web API models
/// in the GetAllCarts use case.
/// </summary>
/// <remarks>
/// Defines mappings between:
/// - <see cref="GetAllCartsResult"/> and <see cref="GetAllCartsResponse"/>
/// - <see cref="GetAllCartsRequest"/> and <see cref="GetAllCartsCommand"/>
/// </remarks>
public class GetAllCartsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCartsProfile"/> class
    /// and configures mappings for the GetAllCarts operation.
    /// </summary>
    public GetAllCartsProfile()
    {
        // Application → WebApi
        CreateMap<GetAllCartsResult, GetAllCartsResponse>();

        // WebApi → Application
        CreateMap<GetAllCartsRequest, GetAllCartsCommand>();

        CreateMap<GetAllCartstItemResult, GetAllCartsItemResponse>()
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount));
    }
}
