using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Carts;

/// <summary>
/// AutoMapper profile for mapping between Guid and DeleteCartCommand
/// </summary>
/// <remarks>
/// Enables mapping from Guid to DeleteCartCommand for controller usage.
/// </remarks>
public class DeleteCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartProfile"/> class
    /// and configures mappings for DeleteCart feature.
    /// </summary>
    public DeleteCartProfile()
    {
        CreateMap<Guid, Application.Carts.DeleteCart.DeleteCartCommand>()
            .ConstructUsing(id => new Application.Carts.DeleteCart.DeleteCartCommand(id));
    }
}
