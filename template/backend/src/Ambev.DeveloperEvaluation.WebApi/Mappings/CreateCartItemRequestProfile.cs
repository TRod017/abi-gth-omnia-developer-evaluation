using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

/// <summary>
/// AutoMapper profile for mapping between <see cref="CreateCartItemRequest"/> (API layer)
/// and <see cref="CreateCartItemCommand"/> (application layer).
/// </summary>
/// <remarks>
/// Defines the mapping used to translate individual cart item data received from the API
/// into application-level commands for cart item creation.
/// </remarks>
public class CreateCartItemRequestProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartItemRequestProfile"/> class
    /// and configures the mapping from request to command.
    /// </summary>
    public CreateCartItemRequestProfile()
    {
        CreateMap<CreateCartItemRequest, CreateCartItemCommand>();
    }
}
