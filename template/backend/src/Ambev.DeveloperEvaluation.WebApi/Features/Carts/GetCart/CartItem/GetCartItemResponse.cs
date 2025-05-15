namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Represents an individual item within the cart returned by the API.
/// </summary>
/// <remarks>
/// Includes product ID, quantity and unit price at the time of addition.
/// </remarks>
public class GetCartItemResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the product associated with the item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product added to the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product when added to the cart.
    /// </summary>
    public decimal UnitPrice { get; set; }
}