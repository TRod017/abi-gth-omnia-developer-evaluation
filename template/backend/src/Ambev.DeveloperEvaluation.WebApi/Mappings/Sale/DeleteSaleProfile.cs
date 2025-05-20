using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

/// <summary>
/// AutoMapper profile for mapping between Guid and DeleteSaleCommand
/// </summary>
/// <remarks>
/// Enables mapping from Guid to DeleteSaleCommand for controller usage.
/// </remarks>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleProfile"/> class
    /// and configures mappings for DeleteSale feature.
    /// </summary>
    public DeleteSaleProfile()
    {
        CreateMap<Guid, Application.Sales.DeleteSale.DeleteSaleCommand>()
            .ConstructUsing(id => new Application.Sales.DeleteSale.DeleteSaleCommand(id));
    }
}
