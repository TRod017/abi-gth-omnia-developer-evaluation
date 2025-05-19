using MediatR;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Command for updating an existing cart.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the updated details of a cart,
/// including the associated user and items. It returns a <see cref="UpdateCartResult"/> upon execution.
/// </remarks>
public class UpdateCartCommand : IRequest<UpdateCartResult>
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
    /// Gets or sets the list of items to be updated in the cart.
    /// </summary>
    public List<UpdateCartItemCommand> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the current status of the cart (e.g., Open, Confirmed).
    /// </summary>
    public CartStatus Status { get; set; }
}
