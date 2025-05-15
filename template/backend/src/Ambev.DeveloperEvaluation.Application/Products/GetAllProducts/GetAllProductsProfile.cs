using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Profile for mapping between Product entity and GetAllProductsResult.
/// </summary>
public class GetAllProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllProducts operation.
    /// </summary>
    public GetAllProductsProfile()
    {
        CreateMap<Product, GetAllProductsResult>();
    }
}
