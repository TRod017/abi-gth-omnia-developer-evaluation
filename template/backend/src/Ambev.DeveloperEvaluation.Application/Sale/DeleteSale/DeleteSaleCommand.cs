using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Command to delete a Sale by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to request the deletion of a Sale.
/// It returns a boolean indicating the success of the operation.
/// </remarks>
public class DeleteSaleCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleCommand"/> class.
    /// Parameterless constructor required for deserialization and model binding.
    /// </summary>
    public DeleteSaleCommand() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleCommand"/> class
    /// with the specified Sale identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Sale to delete.</param>
    public DeleteSaleCommand(Guid id)
    {
        Id = id;
    }
}
