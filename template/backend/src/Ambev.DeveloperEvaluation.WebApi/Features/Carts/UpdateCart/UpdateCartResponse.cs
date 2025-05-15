namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// API response model returned after successfully updating a cart.
/// </summary>
/// <remarks>
/// Contains the unique identifier of the updated cart, which can be used to confirm
/// the update operation or reference the updated resource in subsequent calls.
/// </remarks>
public class UpdateCartResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated cart.
    /// </summary>
    public Guid Id { get; set; }
}
