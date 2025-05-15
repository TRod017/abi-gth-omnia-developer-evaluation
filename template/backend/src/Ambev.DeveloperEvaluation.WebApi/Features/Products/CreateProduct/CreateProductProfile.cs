using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// AutoMapper profile for mapping between Web API models and application layer models
/// in the CreateProduct use case.
/// </summary>
/// <remarks>
/// Defines the mappings used to:
/// - Convert <see cref="CreateProductRequest"/> into <see cref="CreateProductCommand"/>
/// - Convert <see cref="CreateProductResult"/> into <see cref="CreateProductResponse"/>
/// </remarks>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductProfile"/> class
    /// and sets up the mappings for CreateProduct operation.
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductResult, CreateProductResponse>();
    }
}
