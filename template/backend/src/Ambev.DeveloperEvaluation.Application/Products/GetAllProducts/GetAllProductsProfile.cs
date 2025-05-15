using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// AutoMapper profile for mapping between <see cref="Product"/> entity and <see cref="GetAllProductsResult"/>.
/// </summary>
/// <remarks>
/// Defines the mapping used to project product domain entities into response models returned by the
/// GetAllProducts use case.
/// </remarks>
public class GetAllProductsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllProductsProfile"/> class
    /// and configures the mapping from <see cref="Product"/> to <see cref="GetAllProductsResult"/>.
    /// </summary>
    public GetAllProductsProfile()
    {
        CreateMap<Product, GetAllProductsResult>();
    }
}
