namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Represents an individual item within the Sale returned by the API.
/// </summary>
/// <remarks>
/// Includes product ID, quantity, unit price, discount and totals.
/// </remarks>
public class GetSaleItemResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the product associated with the item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the product added to the Sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product when added to the Sale.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage applied to this item (e.g. 0.05 for 5%).
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total price before discount (UnitPrice × Quantity).
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the total price after discount.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }
}

