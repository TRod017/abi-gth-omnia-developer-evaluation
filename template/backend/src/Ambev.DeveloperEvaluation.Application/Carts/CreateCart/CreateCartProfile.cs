using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// AutoMapper profile for <see cref="CreateCartCommand"/> and <see cref="CreateCartResult"/> mappings.
/// </summary>
/// <remarks>
/// This profile defines the mapping rules between the input command used to create a cart
/// and the domain entity <see cref="Cart"/>, as well as the mapping from the entity to the result model.
/// </remarks>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartProfile"/> class
    /// and defines the mappings for CreateCart feature.
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Cart>();
        CreateMap<Cart, CreateCartResult>();
    }
}
