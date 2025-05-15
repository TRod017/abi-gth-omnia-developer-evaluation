using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// AutoMapper profile for mapping between Web API models and application layer models
/// in the CreateCart use case.
/// </summary>
/// <remarks>
/// Defines mappings used to:
/// - Convert <see cref="CreateCartRequest"/> into <see cref="CreateCartCommand"/>
/// - Convert <see cref="CreateCartItemRequest"/> into <see cref="CreateCartItemCommand"/>
/// - Convert <see cref="CreateCartResult"/> into <see cref="CreateCartResponse"/>
/// </remarks>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartProfile"/> class
    /// and configures mappings for the CreateCart operation.
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>();
        CreateMap<CreateCartItemRequest, CreateCartItemCommand>();
        CreateMap<CreateCartResult, CreateCartResponse>();
    }
}
