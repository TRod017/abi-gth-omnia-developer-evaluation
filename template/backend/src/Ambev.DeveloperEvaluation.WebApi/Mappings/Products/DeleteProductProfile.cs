using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Products;

/// <summary>
/// AutoMapper profile for mapping between Web API models and application layer models
/// in the DeleteProduct use case.
/// </summary>
/// <remarks>
/// Defines the mappings used to:
/// - Convert <see cref="Guid"/> (from route) into <see cref="DeleteProductCommand"/>
/// </remarks>
public class DeleteProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductProfile"/> class
    /// and sets up the mappings for DeleteProduct operation.
    /// </summary>
    public DeleteProductProfile()
    {
        CreateMap<Guid, DeleteProductCommand>()
            .ConstructUsing(id => new DeleteProductCommand(id));
    }
}
