using Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItem;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Represents the response model returned after successfully retrieving a cart by its ID.
/// </summary>
/// <remarks>
/// This DTO contains detailed cart information retrieved from the database,
/// including cart ID, associated user ID, status, creation and update timestamps,
/// as well as the list of items in the cart.
/// </remarks>
public class GetCartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the current status of the cart.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the cart, if available.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the list of items contained in the cart.
    /// </summary>
    public List<GetCartItemResult> Items { get; set; } = new();
}
