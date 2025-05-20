namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;

/// <summary>
/// Represents a sale item included in the sale result, with discount breakdown.
/// </summary>
public class CreateSaleItemResult
{
    /// <summary>
    /// Gets or sets the product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the quantity of this item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the total amount before discount.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the value of the discount applied.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total amount after applying discount.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }
}
