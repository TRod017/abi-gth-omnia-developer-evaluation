using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Represents the response model returned for each cart in the GetAllCarts operation.
/// </summary>
/// <remarks>
/// This DTO is used to expose selected cart fields in cart listing endpoints,
/// including the cart ID, associated user, status, and creation/update timestamps.
/// </remarks>
public class GetAllCartsResult
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
    /// Gets or sets the current status of the cart.
    /// </summary>
    public CartStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the cart was last updated, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
