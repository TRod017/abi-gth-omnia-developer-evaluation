using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Represents the request payload used to create a new cart via the API.
/// </summary>
/// <remarks>
/// Includes the user identifier and a collection of cart items to be added.
/// </remarks>
public class CreateCartRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the status of the cart (e.g., Open, Closed).
    /// </summary>
    public CartStatus Status { get; set; } = CartStatus.Open;


    /// <summary>
    /// Gets or sets the list of items to include in the cart.
    /// </summary>
    public List<CreateCartItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Represents a single item in the cart during creation.
/// </summary>
/// <remarks>
/// Contains the product ID, quantity, and unit price at the time of cart creation.
/// </remarks>
public class CreateCartItemRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product to be added to the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time it is added to the cart.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
