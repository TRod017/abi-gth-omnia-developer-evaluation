namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a single Sale item in the update Sale request.
/// </summary>
/// <remarks>
/// Includes updated values such as quantity and unit price for a given product.
/// </remarks>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product in the Sale.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the updated quantity of the product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the updated unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
