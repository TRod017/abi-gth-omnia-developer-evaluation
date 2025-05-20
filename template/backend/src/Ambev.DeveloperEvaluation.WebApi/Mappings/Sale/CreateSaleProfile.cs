using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

/// <summary>
/// AutoMapper profile for mapping between Web API models and application layer models
/// in the CreateSale use case.
/// </summary>
/// <remarks>
/// Defines mappings used to:
/// - Convert <see cref="CreateSaleRequest"/> into <see cref="CreateSaleCommand"/>
/// - Convert <see cref="CreateSaleItemRequest"/> into <see cref="CreateSaleItemCommand"/>
/// - Convert <see cref="CreateSaleResult"/> into <see cref="CreateSaleResponse"/>
/// </remarks>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleProfile"/> class
    /// and configures mappings for the CreateSale operation.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
        CreateMap<CreateSaleItemCommand, SaleItem>();
    }
}
