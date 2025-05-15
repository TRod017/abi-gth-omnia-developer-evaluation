using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// AutoMapper profile for mapping between Web API models and application layer models
/// in the UpdateCart use case.
/// </summary>
/// <remarks>
/// Defines mappings used to:
/// - Convert <see cref="UpdateCartRequest"/> into <see cref="UpdateCartCommand"/>
/// - Convert <see cref="UpdateCartItemRequest"/> into <see cref="UpdateCartItemCommand"/>
/// - Convert <see cref="UpdateCartResult"/> into <see cref="UpdateCartResponse"/>
/// </remarks>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartProfile"/> class
    /// and configures mappings for the UpdateCart operation.
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartRequest, UpdateCartCommand>();
        CreateMap<UpdateCartItemRequest, UpdateCartItemCommand>();
        CreateMap<UpdateCartResult, UpdateCartResponse>();
    }
}
