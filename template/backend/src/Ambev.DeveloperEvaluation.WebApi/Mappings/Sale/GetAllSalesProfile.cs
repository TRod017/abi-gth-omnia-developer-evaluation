using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

/// <summary>
/// AutoMapper profile for mapping between application layer models and Web API models
/// in the GetAllSales use case.
/// </summary>
/// <remarks>
/// Defines mappings between:
/// - <see cref="GetAllSalesResult"/> and <see cref="GetAllSalesResponse"/>
/// - <see cref="GetAllSalesRequest"/> and <see cref="GetAllSalesCommand"/>
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
    }
}
