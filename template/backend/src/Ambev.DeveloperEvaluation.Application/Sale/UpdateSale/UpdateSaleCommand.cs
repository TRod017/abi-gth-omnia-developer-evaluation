using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.SaleItems;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating an existing Sale.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the updated details of a Sale,
/// including the associated user and items. It returns a <see cref="UpdateSaleResult"/> upon execution.
/// </remarks>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the Sale.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of items to be updated in the Sale.
    /// </summary>
    public List<UpdateSaleItemCommand> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the current status of the Sale (e.g., Open, Confirmed).
    /// </summary>
    public string Status { get; set; }
}
