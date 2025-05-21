using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.SaleItems;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

/// <summary>
/// AutoMapper profile for mapping between Web API models and application layer models
/// in the UpdateSale use case.
/// </summary>
/// <remarks>
/// Defines mappings used to:
/// - Convert <see cref="UpdateSaleRequest"/> into <see cref="UpdateSaleCommand"/>
/// - Convert <see cref="UpdateSaleItemRequest"/> into <see cref="UpdateSaleItemCommand"/>
/// - Convert <see cref="UpdateSaleResult"/> into <see cref="UpdateSaleResponse"/>
/// </remarks>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleProfile"/> class
    /// and configures mappings for the UpdateSale operation.
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
        CreateMap<UpdateSaleResult, UpdateSaleResponse>();
    }
}
