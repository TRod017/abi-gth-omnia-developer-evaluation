namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Represents the request payload used to retrieve a cart by its unique identifier.
/// </summary>
/// <remarks>
/// This request is typically sent via the API to retrieve full details of a cart,
/// using the provided <see cref="Id"/> as the lookup key.
/// </remarks>
public class GetCartRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to retrieve.
    /// </summary>
    public Guid Id { get; set; }
}
