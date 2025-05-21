using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale.SaleItems;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// AutoMapper profile for mapping between <see cref="Sale"/> and <see cref="GetSaleResult"/>,
/// as well as between <see cref="SaleItem"/> and <see cref="GetSaleItemResult"/>.
/// </summary>
/// <remarks>
/// Defines the object-object mapping used to convert Sale domain entities
/// into response models used in the GetSale use case.
/// </remarks>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleProfile"/> class
    /// and sets up the mappings for GetSale operation.
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<Sale, GetSaleResult>();
        CreateMap<SaleItem, GetSaleItemResult>();
    }
}
