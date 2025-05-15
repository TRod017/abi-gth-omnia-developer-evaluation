namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

/// <summary>
/// Command to update a specific item within a cart.
/// </summary>
/// <remarks>
/// This command encapsulates the necessary information to update a cart item,
/// including the product ID, quantity, and unit price.
/// </remarks>
public class UpdateCartItemCommand
{
    /// <summary>
    /// Gets or sets the unique identifier of the product being updated in the cart.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time of update.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
