using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Carts;

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
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleProfile"/> class
    /// and configures mappings for the CreateCart operation.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>();
        CreateMap<CreateCartResult, CreateCartResponse>();
        CreateMap<CreateCartItemRequest, CreateCartItemCommand>();
        CreateMap<CreateCartItemCommand, CartItem>();
    }
}
