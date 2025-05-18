namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Represents the request payload used to update an existing cart via the API.
/// </summary>
/// <remarks>
/// Includes the cart ID, updated status, and a list of cart items to update.
/// </remarks>
public class UpdateCartRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the updated status of the cart (e.g., Open, Confirmed).
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of cart items to be updated.
    /// </summary>
    public List<UpdateCartItemRequest> Items { get; set; } = new();
}

