using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// AutoMapper profile for mapping between <see cref="UpdateProductCommand"/>, <see cref="Product"/> entity,
/// and <see cref="UpdateProductResult"/>.
/// </summary>
/// <remarks>
/// Defines the mappings required for the UpdateProduct use case, allowing the system to:
/// - Convert a command into a product entity to persist updates.
/// - Map the updated entity back into a result object to return from the handler.
/// </remarks>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductProfile"/> class
    /// and configures the mappings for the UpdateProduct operation.
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<Product, UpdateProductResult>();
    }
}
