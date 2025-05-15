namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Represents a request to update a cart.
/// </summary>
public class UpdateCartRequest
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<UpdateCartItemRequest> Items { get; set; } = new();
}

public class UpdateCartItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
