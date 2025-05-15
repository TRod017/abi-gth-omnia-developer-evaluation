using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// AutoMapper profile for mapping between <see cref="GetAllProductsResult"/> (application layer)
/// and <see cref="GetAllProductsResponse"/> (API layer).
/// </summary>
/// <remarks>
/// Defines the mapping used to translate application output models
/// into API response models for the GetAllProducts endpoint.
/// </remarks>
public class GetAllProductsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllProductsProfile"/> class
    /// and sets up the mapping from result to response.
    /// </summary>
    public GetAllProductsProfile()
    {
        CreateMap<GetAllProductsResult, GetAllProductsResponse>();
    }
}
