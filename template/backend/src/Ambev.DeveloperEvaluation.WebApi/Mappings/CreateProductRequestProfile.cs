using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

/// <summary>
/// AutoMapper profile for mapping between <see cref="CreateProductRequest"/> (API layer)
/// and <see cref="CreateProductCommand"/> (application layer).
/// </summary>
/// <remarks>
/// Defines the mapping used to translate API input models into
/// commands handled by the application layer for product creation.
/// </remarks>
public class CreateProductRequestProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductRequestProfile"/> class
    /// and configures the mapping from request to command.
    /// </summary>
    public CreateProductRequestProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
    }
}
