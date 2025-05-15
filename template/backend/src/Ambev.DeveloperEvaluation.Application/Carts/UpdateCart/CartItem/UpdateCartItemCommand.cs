namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

/// <summary>
/// Command to update a cart item.
/// </summary>
public class UpdateCartItemCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
