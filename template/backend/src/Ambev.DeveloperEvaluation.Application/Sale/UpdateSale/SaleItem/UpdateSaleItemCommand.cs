namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.SaleItems;

/// <summary>
/// Command to update a specific item within a Sale.
/// </summary>
/// <remarks>
/// This command encapsulates the necessary information to update a Sale item,
/// including the product ID, quantity, and unit price.
/// </remarks>
public class UpdateSaleItemCommand
{
    /// <summary>
    /// Gets or sets the unique identifier of the product being updated in the Sale.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the Sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time of update.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
