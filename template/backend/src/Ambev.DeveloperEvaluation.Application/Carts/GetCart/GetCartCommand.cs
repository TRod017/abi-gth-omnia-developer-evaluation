using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Query to retrieve a cart by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the ID required to retrieve
/// a specific cart. It returns a <see cref="GetCartResult"/> upon execution.
/// </remarks>
public class GetCartCommand : IRequest<GetCartResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to retrieve.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartCommand"/> class.
    /// </summary>
    public GetCartCommand()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartCommand"/> class with a specified cart ID.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    public GetCartCommand(Guid id)
    {
        Id = id;
    }
}
