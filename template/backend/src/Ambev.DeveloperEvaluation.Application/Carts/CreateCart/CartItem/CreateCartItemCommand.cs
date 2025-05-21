using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;

/// <summary>
/// Represents a cart item to be included in a cart creation request.
/// </summary>
/// <remarks>
/// Contains the necessary information about a product added to a cart,
/// such as ProductId, Quantity, and UnitPrice.
/// </remarks>
public class CreateCartItemCommand : IRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time it was added to the cart.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
