using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItems;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// AutoMapper profile for mapping between application layer results and Web API response models
/// in the GetCart use case.
/// </summary>
/// <remarks>
/// Defines mappings used to:
/// - Convert <see cref="GetCartResult"/> to <see cref="GetCartResponse"/>
/// - Convert <see cref="GetCartItemResult"/> to <see cref="GetCartItemResponse"/>
/// </remarks>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartProfile"/> class
    /// and configures mappings for the GetCart operation.
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<GetCartResult, GetCartResponse>();
        CreateMap<GetCartItemResult, GetCartItemResponse>();
        CreateMap<Guid, GetCartCommand>()
            .ConstructUsing(id => new GetCartCommand(id));
    }
}
