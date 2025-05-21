using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

/// <summary>
/// AutoMapper profile for mapping between domain, application layer, and Web API models
/// in the GetAllSales use case.
/// </summary>
/// <remarks>
/// Defines mappings between:
/// - <see cref="GetAllSalesResult"/> and <see cref="GetAllSalesResponse"/>
/// - <see cref="GetAllSalesRequest"/> and <see cref="GetAllSalesCommand"/>
/// - <see cref="Sale"/> and <see cref="GetAllSalesResult"/>
/// - <see cref="SaleItem"/> and <see cref="CreateSaleItemResult"/>
/// </remarks>
public class GetAllSalesProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllSalesProfile"/> class
    /// and configures mappings for the GetAllSales operation.
    /// </summary>
    public GetAllSalesProfile()
    {
        // Application → WebApi
        CreateMap<GetAllSalesResult, GetAllSalesResponse>();

        // WebApi → Application
        CreateMap<GetAllSalesRequest, GetAllSalesCommand>();

        // Domain → Application
        CreateMap<SaleItem, CreateSaleItemResult>();

        CreateMap<Sale, GetAllSalesResult>()
    .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
    .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
    .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount))
    .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled))
    .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
