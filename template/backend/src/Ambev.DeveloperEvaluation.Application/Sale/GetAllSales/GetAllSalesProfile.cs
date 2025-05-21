using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// AutoMapper profile for mapping between <see cref="Sale"/> entity and <see cref="GetAllSalesResult"/>.
/// </summary>
/// <remarks>
/// Defines the mapping used to project Sale domain entities into response models returned by the
/// GetAllSales use case.
/// </remarks>
public class GetAllSalesProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllSalesProfile"/> class
    /// and configures the mapping from <see cref="Sale"/> to <see cref="GetAllSalesResult"/>.
    /// </summary>
    public GetAllSalesProfile()
    {
        CreateMap<Sale, GetAllSalesResult>();
    }
}
