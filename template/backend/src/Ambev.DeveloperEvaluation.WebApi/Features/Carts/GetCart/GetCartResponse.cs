namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// API response model for retrieving a single cart by its unique identifier.
/// </summary>
/// <remarks>
/// Includes full cart information, such as user ID, status, timestamps,
/// and a list of cart items.
/// </remarks>
public class GetCartResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the current status of the cart (e.g., Open, Confirmed).
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp indicating when the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the cart was last updated, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the list of items contained within the cart.
    /// </summary>
    public List<GetCartItemResponse> Items { get; set; } = new();
}

