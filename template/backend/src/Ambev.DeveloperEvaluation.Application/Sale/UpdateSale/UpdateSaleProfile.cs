using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.SaleItems;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// AutoMapper profile for mapping between <see cref="UpdateSaleCommand"/>, <see cref="Sale"/> entity,
/// <see cref="UpdateSaleItemCommand"/>, <see cref="SaleItem"/>, and <see cref="UpdateSaleResult"/>.
/// </summary>
/// <remarks>
/// Defines the mappings required for the UpdateSale use case, allowing the system to:
/// - Convert update commands into domain entities for persistence.
/// - Map updated entities back into result objects returned by the handler.
/// </remarks>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleProfile"/> class
    /// and configures the mappings for the UpdateSale operation.
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateSaleItemCommand, SaleItem>()
            .ForMember(dest => dest.SaleId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductName, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Sale, UpdateSaleResult>();
    }
}
