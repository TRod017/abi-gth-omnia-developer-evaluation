namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItems;


/// <summary>
/// Represents an item in the cart result.
/// </summary>
public class GetCartItemResult
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
