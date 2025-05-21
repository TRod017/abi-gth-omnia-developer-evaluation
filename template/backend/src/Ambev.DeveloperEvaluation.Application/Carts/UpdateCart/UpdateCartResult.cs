namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Represents the response model returned after successfully updating a cart.
/// </summary>
/// <remarks>
/// This DTO contains the unique identifier of the updated cart, typically used
/// to confirm the update operation and reference the cart in subsequent actions.
/// </remarks>
public class UpdateCartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated cart.
    /// </summary>
    public Guid Id { get; set; }
}
