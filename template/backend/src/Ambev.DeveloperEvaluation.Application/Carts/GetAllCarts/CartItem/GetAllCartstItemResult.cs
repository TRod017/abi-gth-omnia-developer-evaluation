namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts.CartItem;

/// <summary>
/// Represents an item in the cart, including calculated pricing and discount details.
/// </summary>
public class GetAllCartstItemResult
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
    /// Gets or sets the quantity of the product added to the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time it was added to the cart.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the total value of the item before any discounts.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to the item based on business rules.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the final total value after applying the discount.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }
}
