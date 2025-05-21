using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// AutoMapper profile for mapping between <see cref="Product"/> entity and <see cref="GetProductResult"/>.
/// </summary>
/// <remarks>
/// Defines the object-object mapping used to convert a domain-level product entity
/// into the response model used in the GetProduct use case.
/// </remarks>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductProfile"/> class
    /// and sets up the mapping from <see cref="Product"/> to <see cref="GetProductResult"/>.
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>();
    }
}
