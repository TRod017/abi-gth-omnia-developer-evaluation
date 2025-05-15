namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Represents the response returned after successfully creating a new cart.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created cart,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateCartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created cart.
    /// </summary>
    public Guid Id { get; set; }
}
