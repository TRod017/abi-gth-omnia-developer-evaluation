namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Represents the response returned after successfully creating a new cart.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created cart,
/// as well as its calculated total, status, and item-level financial breakdown.
/// </remarks>
public class CreateCartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the total value of the cart after applying item discounts.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the status of the cart (e.g., Open, Confirmed).
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation date of the cart.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the cart.
    /// </summary>
    public List<CreateCartItemResult> Items { get; set; } = new();
}
