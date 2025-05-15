namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItems;

/// <summary>
/// Represents the response model for an individual item in a cart.
/// </summary>
/// <remarks>
/// This DTO contains essential information about a cart item,
/// including the associated product ID, quantity selected, and unit price at the time of inclusion.
/// </remarks>
public class GetCartItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product associated with this cart item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product added to the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time it was added to the cart.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
