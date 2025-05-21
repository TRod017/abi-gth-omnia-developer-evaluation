using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Represents the response returned by the API after successfully creating a cart.
/// </summary>
/// <remarks>
/// Contains all details of the newly created cart, including its identifier, total,
/// status, creation timestamp, and the list of items with financial details.
/// </remarks>
public class CreateCartResponse
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
    public List<CreateCartItemResponse> Items { get; set; } = new();
}
