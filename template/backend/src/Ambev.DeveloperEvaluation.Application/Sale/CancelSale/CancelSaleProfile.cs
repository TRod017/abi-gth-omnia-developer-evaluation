using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using SharpCompress.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// AutoMapper profile for mapping between <see cref="CancelSaleCommand"/>, <see cref="Sale"/> entity,
/// <see cref="UpdateSaleItemCommand"/>, <see cref="SaleItem"/>, and <see cref="CancelSaleResult"/>.
/// </summary>
/// <remarks>
/// Defines the mappings required for the CancelSale use case, allowing the system to:
/// - Convert cancel commands into domain entities for persistence.
/// - Map updated entities back into result objects returned by the handler.
/// </remarks>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleProfile"/> class
    /// and configures the mappings for the CancelSale operation.
    /// </summary>
    public CancelSaleProfile()
    {
        // CancelSaleCommand --> Sale(entity)
        CreateMap<CancelSaleCommand, Sale>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.Ignore()); // garante que itens não sejam sobrescritos

        // Sale --> CancelSaleResult (result)
        CreateMap<Sale, CancelSaleResult>();
    }
}
