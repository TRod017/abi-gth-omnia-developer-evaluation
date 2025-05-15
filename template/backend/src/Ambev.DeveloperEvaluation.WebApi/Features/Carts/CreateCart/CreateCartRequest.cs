namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Represents a request to create a new cart with associated user and items.
/// </summary>
public class CreateCartRequest
{
    public Guid UserId { get; set; }

    public List<CreateCartItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Represents a single item in the cart creation request.
/// </summary>
public class CreateCartItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
