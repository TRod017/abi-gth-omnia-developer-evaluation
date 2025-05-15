using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// AutoMapper profile for <see cref="CreateProductCommand"/> and <see cref="CreateProductResult"/> mappings.
/// </summary>
/// <remarks>
/// This profile defines the mapping rules between the input command used to create a product
/// and the domain entity <see cref="Product"/>, as well as the mapping from the entity to the result model.
/// </remarks>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductProfile"/> class
    /// and defines the mappings for CreateProduct feature.
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductResult>();
    }
}
