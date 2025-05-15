using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// AutoMapper profile for mapping between <see cref="GetProductResult"/> (application layer)
/// and <see cref="GetProductResponse"/> (API layer).
/// </summary>
/// <remarks>
/// Defines the mapping used to translate the result of a GetProduct query
/// into the API response model exposed to clients.
/// </remarks>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductProfile"/> class
    /// and configures the mapping from result to response.
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<GetProductResult, GetProductResponse>();
    }
}
