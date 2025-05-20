using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale.SaleItems;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

/// <summary>
/// AutoMapper profile for mapping between application layer results and Web API response models
/// in the GetSale use case.
/// </summary>
/// <remarks>
/// Defines mappings used to:
/// - Convert <see cref="GetSaleResult"/> to <see cref="GetSaleResponse"/>
/// - Convert <see cref="GetSaleItemResult"/> to <see cref="GetSaleItemResponse"/>
/// </remarks>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleProfile"/> class
    /// and configures mappings for the GetSale operation.
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<GetSaleResult, GetSaleResponse>();

        CreateMap<GetSaleItemResult, GetSaleItemResponse>()
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount));

        CreateMap<Guid, GetSaleCommand>()
            .ConstructUsing(id => new GetSaleCommand(id));
    }
}
