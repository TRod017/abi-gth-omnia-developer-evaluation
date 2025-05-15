using Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItems;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Represents the result of retrieving a cart.
/// </summary>
public class GetCartResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<GetCartItemResult> Items { get; set; } = new();
}

