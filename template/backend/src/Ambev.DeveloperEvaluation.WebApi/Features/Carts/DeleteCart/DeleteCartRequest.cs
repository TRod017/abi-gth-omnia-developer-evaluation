namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Represents the request payload used to delete a cart by its unique identifier.
/// </summary>
/// <remarks>
/// This request is typically sent via the API to initiate the deletion of a cart,
/// using the provided <see cref="Id"/> as reference.
/// </remarks>
public class DeleteCartRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to be deleted.
    /// </summary>
    public Guid Id { get; set; }
}
