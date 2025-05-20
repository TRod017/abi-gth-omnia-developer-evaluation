using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Query to retrieve a Sale by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the ID required to retrieve
/// a specific Sale. It returns a <see cref="GetSaleResult"/> upon execution.
/// </remarks>
public class GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale to retrieve.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleCommand"/> class.
    /// </summary>
    public GetSaleCommand()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleCommand"/> class with a specified Sale ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Sale.</param>
    public GetSaleCommand(Guid id)
    {
        Id = id;
    }
}
