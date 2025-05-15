namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Represents the response returned by the API after successfully creating a cart.
/// </summary>
/// <remarks>
/// Contains the unique identifier of the newly created cart, which can be used
/// for further operations such as retrieval or updates.
/// </remarks>
public class CreateCartResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created cart.
    /// </summary>
    public Guid Id { get; set; }
}
