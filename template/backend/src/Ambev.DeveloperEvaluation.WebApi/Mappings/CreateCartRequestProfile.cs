using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

/// <summary>
/// AutoMapper profile for mapping between <see cref="CreateCartRequest"/> (API layer)
/// and <see cref="CreateCartCommand"/> (application layer).
/// </summary>
/// <remarks>
/// Defines the mapping used to translate API input models into
/// commands handled by the application layer for cart creation.
/// </remarks>
public class CreateCartRequestProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartRequestProfile"/> class
    /// and configures the mapping from request to command.
    /// </summary>
    public CreateCartRequestProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>();
    }
}
