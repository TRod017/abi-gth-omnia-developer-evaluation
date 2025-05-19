using MediatR;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Command for creating a new cart with optional items and user association.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the necessary information for creating a new cart,
/// including the user ID and a collection of cart items. It implements <see cref="IRequest{TResponse}"/>
/// to return a <see cref="CreateCartResult"/> upon execution. The input data is validated through
/// the <see cref="CreateCartValidator"/> class.
/// </remarks>
public class CreateCartCommand : IRequest<CreateCartResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the current status of the cart.
    /// </summary>
    public CartStatus Status { get; set; } = CartStatus.Open;

    /// <summary>
    /// Gets or sets the list of cart items included in the cart.
    /// </summary>
    public List<CreateCartItemCommand> Items { get; set; } = new();
}
