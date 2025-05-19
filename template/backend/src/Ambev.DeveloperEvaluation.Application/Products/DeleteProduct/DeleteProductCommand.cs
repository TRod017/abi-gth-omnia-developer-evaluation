using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command to delete a product by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to request the deletion of a product. 
/// It returns a boolean indicating the success of the operation.
/// </remarks>
public class DeleteProductCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductCommand"/> class.
    /// This parameterless constructor is required for model binding and deserialization.
    /// </summary>
    public DeleteProductCommand() { }

    /// <summary>
    /// Initializes a new instance of <see cref="DeleteProductCommand"/> with the specified product ID.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}
