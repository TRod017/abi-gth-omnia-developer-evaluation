using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale based on a confirmed Sale.
/// </summary>
/// <remarks>
/// This command encapsulates the necessary information for creating a new sale,
/// including the Sale ID used as the source, the user ID, and the list of items
/// derived from the Sale. It implements <see cref="IRequest{TResponse}"/> to return
/// a <see cref="CreateSaleResult"/> upon execution. The input data is validated through
/// the <see cref="CreateSaleValidator"/> class.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the user making the purchase.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the cart from which the sale is created.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the sale.
    /// </summary>
    public List<CreateSaleItemCommand> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the sale number for identification purposes.
    /// Typically a unique business identifier such as "VEN-0001".
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time the sale was created.
    /// </summary>
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the name of the branch where the sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the sale has been marked as cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;
}
