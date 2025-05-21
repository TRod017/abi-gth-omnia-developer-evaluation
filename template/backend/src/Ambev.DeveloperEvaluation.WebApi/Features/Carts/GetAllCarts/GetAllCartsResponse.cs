namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

/// <summary>
/// Represents a summary view of a cart in a list returned by the GetAllCarts endpoint.
/// </summary>
/// <remarks>
/// Includes essential information such as cart ID, user ID, status and creation timestamp.
/// </remarks>
public class GetAllCartsResponse
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
    /// Gets or sets the date and time the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
