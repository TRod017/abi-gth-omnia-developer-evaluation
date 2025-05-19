using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Query to retrieve a product by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the ID required to retrieve
/// a specific product. It returns a <see cref="GetProductResult"/> upon execution.
/// </remarks>
public class GetProductCommand : IRequest<GetProductResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductCommand"/> class.
    /// </summary>
    public GetProductCommand() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductCommand"/> class with a specified ID.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    public GetProductCommand(Guid id)
    {
        Id = id;
    }
}
