using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// AutoMapper profile for mapping between Web API models and application layer models
/// in the UpdateProduct use case.
/// </summary>
/// <remarks>
/// Defines the mappings used to:
/// - Convert <see cref="UpdateProductRequest"/> into <see cref="UpdateProductCommand"/>
/// - Convert <see cref="UpdateProductResult"/> into <see cref="UpdateProductResponse"/>
/// </remarks>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductProfile"/> class
    /// and configures the mappings for the UpdateProduct operation.
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
        CreateMap<UpdateProductResult, UpdateProductResponse>();
    }
}
